using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Requests
{
    public class ExerciseCategoryRequests
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
