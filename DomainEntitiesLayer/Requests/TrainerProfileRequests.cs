using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Requests
{
    public class TrainerProfileRequests
    {
        public int? Id { get; set; }
        [Required]
        public int? TrainerId { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+963|09)[0-9]{8}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "The value must be either 'Male' or 'Female'.")]
        public string Gender { get; set; }
        [Required]
        [Range(10,70)]
        public short Age { get; set; }

        public string? Description { get; set; }

        public string? Experience { get; set; }

        public string? Championship { get; set; }

        public IFormFile? ImageFile { get; set; }

    }
}
