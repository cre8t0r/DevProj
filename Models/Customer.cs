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
        [StringLength(80, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required(ErrorMessage = "Name must have at least 2 characters. (e.g. JT or BO)")]
        public string Name { get; set; }

        //[StringLength(3, MinimumLength = 1)]
        [Display(Name = "Age")]
        [RegularExpression(@"^[0-9]*$")]
        [Range(1, 150)]
        [Required(ErrorMessage = "Age must be a numerical value (1  to 150).")]
        public int Age { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A valid date of birth is required. (dd/MM/yyyy)")]
        //public DateTime DateOfBirth { get; set; }   
        public DateTime DateOfBirth { get; set; }

        //[StringLength(100, MinimumLength = 5)]
        public String Occupation { get; set; }

        [Display(Name = "Sum Insured")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1000, 1000000)]
        [Required(ErrorMessage = "Valid value required for Sum Insured (1,000 to 1,000,000).")]
        public decimal SumInsured { get; set; }

        [Display(Name = "Monthly Expenses Total")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Valid value required for Monthly Expenses Total.")]
        public decimal MonthlyExpensesTotal { get; set; }

        [Display(Name = "State")]
        //[StringLength(7, MinimumLength = 7)]
        public string State { get; set; }

        [Display(Name = "Postcode")]
        [Range(0200, 9999)]
        //[RegularExpression(@"^[0-9]{4}$")]
        [Required(ErrorMessage = "Postcode needs to be 4 digits (0200 to 9999).")]
        public int Postcode { get; set; }

        //[Display(Name = "Total Value")]
        //public decimal TotalValue { get; set; }
    }
}
