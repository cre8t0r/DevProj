using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models
{
    [Serializable()]
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        //[StringLength(80, MinimumLength = 2)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required(ErrorMessage = "Name must be at least 2 characters and first chraracter must be capital.")]
        //[Required]
        public string Name { get; set; }

        //[StringLength(3, MinimumLength = 1)]
        [RegularExpression(@"^[0-9]*$")]
        [Range(1, 150)]
        [Required(ErrorMessage = "Age must be a numerical value.")]
        public int Age { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A valid date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        //[StringLength(100, MinimumLength = 5)]
        public String Occupation { get; set; }

        [Display(Name = "Sum Insured")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1000, 1000000)]
        public decimal SumInsured { get; set; }

        [Display(Name = "Monthly Expenses Total")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MonthlyExpensesTotal { get; set; }

        //[StringLength(7, MinimumLength = 7)]
        public string State { get; set; }

        [Range(0200, 9999)]
        //[StringLength(4, MinimumLength = 4)]
        [RegularExpression(@"^[0-9]{4}$,")]
        public int Postcode { get; set; }

        //[Display(Name = "Total Value")]
        //public decimal TotalValue { get; set; }
    }
}
