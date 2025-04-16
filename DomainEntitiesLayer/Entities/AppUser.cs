using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DomainEntitiesLayer.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Range(10,60)]
        public short Age { get; set; }
        public string? Image {  get; set; }
        public bool IsActive { get; set; }


    }
}
