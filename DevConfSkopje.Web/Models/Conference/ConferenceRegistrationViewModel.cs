using System.ComponentModel.DataAnnotations;

namespace DevConfSkopje.Web.Models.Conference
{
    public class ConferenceRegistrationViewModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(50,MinimumLength = 2, ErrorMessage = "First name must be above 2 characters !")]
        [RegularExpression(@"^[a-zA-Z]+(([.'-][a-zA-Z])?[a-zA-Z]*)*$", ErrorMessage = "First name cannot contain numbers or special characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 2,ErrorMessage = "Last name must be above 2 characters !")]
        [RegularExpression(@"^[a-zA-Z]+(([.'-][a-zA-Z])?[a-zA-Z]*)*$", ErrorMessage = "Last name cannot contain numbers or another special characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(15,MinimumLength = 8,ErrorMessage = "Phone number must contain at least 8 to 15 characters !")]
        public string PhoneNumber { get; set; }

        public bool isValid { get; set; }
    }
}