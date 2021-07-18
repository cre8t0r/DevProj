using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models
{
    public class OccupationRating
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Occupation is required.")]
        public string Occupation { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Rating is required.")]
        public string Rating { get; set; }
    }
}
