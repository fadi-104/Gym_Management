using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class Exercise : BaseEntity
    {
        public int? CategoryId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public string? Image {  get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ExerciseCategory Category { get; set; }
    }
}
