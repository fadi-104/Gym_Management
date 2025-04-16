using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DomainEntitiesLayer.Requests
{
    public class UserRequests
    {

        public int? Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?:\+963|09)[0-9]{8}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "The value must be either 'Male' or 'Female'.")]
        public string Gender { get; set; }
        [Required]
        [Range(10,70)]
        public short Age { get; set; }
        [Required]
        public string Role { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
