using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend.Models.Authentication
{
    public class UserResetPassword
    {
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(20, ErrorMessage = "Must be between 6 and 20 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [StringLength(20, ErrorMessage = "Must be between 6 and 20 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Password confirmation do not match")]
        public string ConfirmPassword { get; set; }
    }
}
