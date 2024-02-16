using System.ComponentModel.DataAnnotations;

namespace Zevopay.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Email ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
