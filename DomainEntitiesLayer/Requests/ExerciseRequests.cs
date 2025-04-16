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
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }

    }
}
