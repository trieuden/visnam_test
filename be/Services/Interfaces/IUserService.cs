using be.DTOs;

namespace be.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> LoginAsync(UserLoginDto request);
        Task<UserResponseDto?> GetMe();
    }
}