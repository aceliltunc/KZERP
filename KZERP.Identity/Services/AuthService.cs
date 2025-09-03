using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KZERP.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwt;

        public AuthService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IJwtTokenService jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
        }

        public async Task<(bool ok, IEnumerable<string> errors)> RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser
            {
                UserName   = dto.Username,
                Email      = dto.Email,
                FullName   = dto.FullName,
                JobTitle   = dto.JobTitle,
                Department = dto.Department,
                IsActive   = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return (false, result.Errors.Select(e => e.Description));

            // Default rol (opsiyonel)
            await _userManager.AddToRoleAsync(user, "User" /* ya da "Worker" */);
            return (true, Array.Empty<string>());
        }

        public async Task<(bool ok, string? token, string? error)> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UsernameOrEmail)
                    ?? await _userManager.FindByEmailAsync(dto.UsernameOrEmail);

            if (user is null) return (false, null, "User not found");

            var passwordOk = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!passwordOk.Succeeded) return (false, null, "Invalid password");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim("FullName", user.FullName ?? string.Empty),
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = _jwt.GenerateToken(claims);
            return (true, token, null);
        }
    }
}
