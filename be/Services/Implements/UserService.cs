using be.Data;
using be.DTOs;
using be.Models;
using be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using be.DTOs.be.DTOs;

namespace be.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> LoginAsync(UserLoginDto request)
        {
            var user = await _context.Users
                .Include(u => u.role) // Model User: Role (Viết hoa)
                .FirstOrDefaultAsync(u => u.username == request.username); // Model User: Username (Viết hoa)

            // Model User: PasswordHash (Viết hoa)
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.password, user.passwordHash))
            {
                return null;
            }

            return CreateToken(user);
        }

        public async Task<UserResponseDto?> GetMe()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var user = await _context.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.id == userId); // Model User: Id (Viết hoa)

            if (user == null) return null;

            return new UserResponseDto
            {
                // Bên trái là DTO (chữ thường), Bên phải là Entity (Viết Hoa)
                id = user.id,
                username = user.username,
                email = user.email,
                role = new RoleDto
                {
                    id = user.role!.id,
                    name = user.role!.name
                }
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Email, user.email)
            };

            if (user.role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.role.name));
            }

            // Lấy key từ appsettings.json
            var keyString = _configuration.GetSection("Jwt:Token").Value;

            // Kiểm tra nếu key chưa cấu hình thì báo lỗi hoặc dùng key tạm (chỉ khi dev)
            if (string.IsNullOrEmpty(keyString))
            {
                throw new Exception("JWT Key is not configured in appsettings.json");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}