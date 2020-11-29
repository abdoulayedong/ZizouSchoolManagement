using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.API.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} must be at least {3} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} must be at least {3} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
