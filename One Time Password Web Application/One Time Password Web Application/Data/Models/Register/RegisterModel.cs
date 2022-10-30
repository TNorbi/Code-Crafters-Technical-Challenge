using System.ComponentModel.DataAnnotations;

namespace One_Time_Password_Web_Application.Data.Models.Register
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20,MinimumLength = 5, ErrorMessage = "Last Name cannot have less than 5 characters and more than 20 characters in length")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Password cannot have less than 5 characters and more than 16 characters in length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Confirm Password cannot have less than 5 characters and more than 16 characters in length")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
        public string ConfirmPassword { get; set; }
    }
}
