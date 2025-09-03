using Microsoft.AspNetCore.Identity;

namespace KZERP.Identity.AppUser
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}