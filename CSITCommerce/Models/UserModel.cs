using System.ComponentModel.DataAnnotations;

namespace CSITCommerce.Models
{
    public class UserModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
