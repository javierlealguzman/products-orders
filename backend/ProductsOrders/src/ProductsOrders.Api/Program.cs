using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsOrders.Api.Middlewares;
using ProductsOrders.Application.Common.Interfaces;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Application.Common.Settings;
using ProductsOrders.Application.Services;
using ProductsOrders.Domain.Factories;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Domain.Strategies;
using ProductsOrders.Infrastructure.Auth;
using ProductsOrders.Infrastructure.Factories;
using ProductsOrders.Infrastructure.Payments;
using ProductsOrders.Infrastructure.Persistence;
using ProductsOrders.Infrastructure.Persistence.Repositories;
using ProductsOrders.Infrastructure.Persistence.Seed;
using ProductsOrders.Infrastructure.Providers;
using ProductsOrders.Infrastructure.Providers.Strategies;
using ProductsOrders.Infrastructure.Security;
using ProductsOrders.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentOrderService, PaymentOrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IProviderStrategy, CazaPagosStrategy>();
builder.Services.AddScoped<IProviderStrategy, PagoFacilStrategy>();

builder.Services.AddScoped<IProviderSelector, ProviderSelector>();

builder.Services.AddSingleton<IExternalPaymentClient, PagoFacilExternalPaymentClient>();
builder.Services.AddSingleton<IExternalPaymentClient, CazaPagoExternalPaymentClient>();
builder.Services.AddSingleton<IProviderFactory, ProviderFactory>();

builder.Services.AddScoped<DbSeed>();

builder.Services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowBlazorClient", policy => {
        policy.WithOrigins("https://localhost:7089")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with Bearer prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<ProductsOrdersContext>(options => {
    options.UseInMemoryDatabase("MyProdutsOrdersDatabase");
});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!))
    };
});

var providers = config.GetSection("Providers").GetChildren();
var providersDictionary = providers.ToDictionary(x => x.Key, x => x.Get<ProviderSettings>());

foreach (var provider in providersDictionary)
{
    builder.Services.AddHttpClient(provider.Key, client =>
    {
        client.BaseAddress = new Uri(provider.Value!.Url);
        foreach (var header in provider.Value.Headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    });
}

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeed>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorClient");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }