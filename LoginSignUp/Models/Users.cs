using System.ComponentModel.DataAnnotations;

namespace LoginSignUp.Models
{
    public class Users
    {
        [Key] 
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "UserName must be between 1 and 50 characters", MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password must be between 6 and 50 characters", MinimumLength = 6)]
        public string UserPassword { get; set; }

        public bool IsActive { get; set; }
    }
}
