using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DomainEntitiesLayer.Requests
{
    public class ExerciseRequests
    {
        
        public int? Id { get; set; }

        public int? CategoryId { get; set; }
        
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }
        
        public IFormFile? ImageFile { get; set; }

    }
}
