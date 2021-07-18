using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models.Calculator
{
    [Serializable()]
    public class CalculatorPage2
    {
        //private List<String> _state = new List<string>();

        //[StringLength(100, MinimumLength = 5)]
        public String Occupation { get; set; }

        [Display(Name = "Sum Insured")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1000, 1000000)]
        [Required(ErrorMessage = "Valid value required for Sum Insured (1,000 to 1,000,000).")]
        public decimal? SumInsured { get; set; }

        [Display(Name = "Monthly Expenses Total")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Valid value required for Monthly Expenses Total.")]
        public decimal? MonthlyExpensesTotal { get; set; }

        [Display(Name = "State")]
        //[StringLength(7, MinimumLength = 7)]
        public string State { get; set; }

        [Display(Name = "Postcode")]
        [Range(0200, 9999)]              
        //[RegularExpression(@"^[0-9]{4}$")]
        [Required(ErrorMessage = "Postcode needs to be 4 digits (0200 to 9999).")]
        public int? Postcode { get; set; }

        //[Display(Name = "Total Value")]
        //public decimal TotalValue { get; set; }
    }
}
