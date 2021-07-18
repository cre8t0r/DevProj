using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models
{
    public class RatingFactor
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Rating { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Factor { get; set; }
    }
}
