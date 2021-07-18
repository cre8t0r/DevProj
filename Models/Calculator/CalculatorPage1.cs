using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models.Calculator
{
    [Serializable()]
    public class CalculatorPage1
    {
        private DateTime _dob = new DateTime();
        private int _age = 0;

        [Display(Name = "Customer Name")]
        //[StringLength(80, MinimumLength = 2)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required(ErrorMessage = "Name must be at least 2 characters and first chraracter must be capital.")]
        //[Required]
        public string Name { get; set; }

        //[StringLength(3, MinimumLength = 1)]
        [Display(Name = "Age")]
        [RegularExpression(@"^[0-9]*$")]
        [Range(1, 150)]
        [Required(ErrorMessage = "Age must be a numerical value.")]
        public int? Age { get; set; }
        //public int Age 
        //{
        //    get
        //    {
        //        if (this.DateOfBirth > DateTime.Now)
        //        {
        //            DateTime.TryParse(this.DateOfBirth.ToString(), out _dob);
        //            if (_dob > DateTime.Now)
        //            {
        //                _age = DateTime.Now.Year - _dob.Year;
        //            }                    
        //        }
        //        return _age;
        //    }
        //    set
        //    {
        //        _age = value;
        //    }
        //}

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A valid date of birth is required.")]
        //public DateTime DateOfBirth { get; set; }   
        public string DateOfBirth { get; set; }
        //public DateTime DateOfBirth 
        //{
        //    get
        //    {
        //        if (this.Age > 0)
        //        {
        //            _dob = DateTime.Now.AddYears(-(DateTime.Now.Year - this.Age));
        //        }
        //        return _dob;
        //    }
        //    set
        //    {
        //        _dob = value;
        //    }
        //}
    }
}
