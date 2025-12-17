using be.DTOs;
using be.DTOs.be.DTOs;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> LoginAsync(UserLoginDto request);
        Task<UserResponseDto?> GetMe();
    }
}