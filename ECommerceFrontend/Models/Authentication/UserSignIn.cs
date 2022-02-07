using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend.Models.Authentication
{
    public class UserSignIn
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
