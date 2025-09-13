using System.ComponentModel.DataAnnotations;

namespace KZERP.MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}