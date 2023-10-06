using API.Dtos;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Services;
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Email = registerDto.Email,
            Usuario = registerDto.Usuario
        };

        user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

        var existingUser = _unitOfWork.Users
                                    .Find(u => u.Usuario.ToLower() == registerDto.Usuario.ToLower())
                                    .FirstOrDefault();

        if (existingUser == null)
        {
            try
            {
                _unitOfWork.Users.Add(user);
                await _unitOfWork.SaveAsync();

                return $"User  {registerDto.Usuario} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"User {registerDto.Usuario} already registered.";
        }
    }
}
