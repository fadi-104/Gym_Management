using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class TrainerProfileResponses
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email {  get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public short Age { get; set; }

        public string Description { get; set; }

        public string Experience { get; set; }

        public string Championship { get; set; }

        public string? Image { get; set; }
    }
}
