using KZERP.Identity;
using System.Threading.Tasks;

namespace KZERP.Identity.Services
{
    public record RegisterDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
    }
    public class LoginDto
    {
        public string? UsernameOrEmail { get; set; }
        public string? Password { get; set; }
    }

    public interface IAuthService
    {
        Task<(bool ok, IEnumerable<string> errors)> RegisterAsync(RegisterDto dto);
        Task<(bool ok, string? token, string? error)> LoginAsync(LoginDto dto);
    }
}
