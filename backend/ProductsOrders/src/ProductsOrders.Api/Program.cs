using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsOrders.Application.Common.Interfaces;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Application.Common.Settings;
using ProductsOrders.Application.Services;
using ProductsOrders.Infrastructure.Auth;
using ProductsOrders.Infrastructure.Persistence;
using ProductsOrders.Infrastructure.Persistence.Repositories;
using ProductsOrders.Infrastructure.Persistence.Seed;
using ProductsOrders.Infrastructure.Security;
using ProductsOrders.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<DbSeed>();

builder.Services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductsOrdersContext>(options => {
    options.UseInMemoryDatabase("MyProdutsOrdersDatabase");
});

builder.Services.AddAuthentication().AddJwtBearer(options => {
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

builder.Services.AddAuthorization();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
