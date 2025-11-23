using ProductsOrders.Application.Common.Interfaces;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Application.Services;

namespace ProductsOrders.Infrastructure.Services;

public class AuthService(
    IUserRepository userRepository, 
    IJwtProvider jwtProvider, 
    IPasswordHasher hasher) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordHasher _hasher = hasher;

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username) 
            ?? throw new Exception("User not found");

        if (!_hasher.Verify(password, user.PasswordHash)) 
            throw new Exception("Invalid credentials");

        return _jwtProvider.GenerateToken(user.Id, user.Username, user.Role);
    }
}