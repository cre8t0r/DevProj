using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentProject.Models.Calculator
{
    [Serializable()]
    public class CalculatorViewModel
    {
        public CalculatorPage1 CalculatorPage1 { get; set; }
        public CalculatorPage2 CalculatorPage2 { get; set; }

        //public Customer Customer { get; set; }
        public List<OccupationRating> OccupationRatings { get; set; }
    }
}
