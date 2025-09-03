using KZERP.Identity;
using System.Threading.Tasks;

namespace KZERP.Identity.Services
{
    public record RegisterDto(string Username, string Email, string Password, string? FullName, string? JobTitle, string? Department);
    public record LoginDto(string UsernameOrEmail, string Password);

    public interface IAuthService
    {
        Task<(bool ok, IEnumerable<string> errors)> RegisterAsync(RegisterDto dto);
        Task<(bool ok, string? token, string? error)> LoginAsync(LoginDto dto);
    }
}
