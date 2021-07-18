using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DevelopmentProject.Data;
using DevelopmentProject.Models;
using DevelopmentProject.Models.Calculator;

namespace DevelopmentProject.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly DevProjMainContext _context;

        public CalculatorController(DevProjMainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // POST: Calculator/CalPage1        
        public IActionResult Page1()
        {
            return View("CalPage1");
        }

        [HttpGet]
        // POST: Calculator/CalPage2       
        public IActionResult Page2()
        {
            return View("CalPage2");
        }

        /// <summary>
        /// Display Page 1
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        // POST: Calculator/CalPage1        
        public IActionResult Page1(Models.Calculator.CalculatorViewModel viewModel)
        {
            #region This logic is to retain the values from Page 2

            String occupation = "";
            decimal sumInsured = 0;
            decimal monthlyEpensesTotal = 0;
            String state = "";
            int postcode = 0;
            decimal totalValue = 0;

            if (!String.IsNullOrWhiteSpace(viewModel.CalculatorPage2.Occupation))
            {
                occupation = viewModel.CalculatorPage2.Occupation;
                viewModel.CalculatorPage1.Occupation = occupation;
            }

            if (viewModel.CalculatorPage2.SumInsured != null)
            {
                Decimal.TryParse(viewModel.CalculatorPage2.SumInsured.GetValueOrDefault().ToString(), out sumInsured);
                viewModel.CalculatorPage1.SumInsured = sumInsured;
            }

            if (viewModel.CalculatorPage2.MonthlyExpensesTotal != null)
            {
                Decimal.TryParse(viewModel.CalculatorPage2.MonthlyExpensesTotal.GetValueOrDefault().ToString(), out monthlyEpensesTotal);
                viewModel.CalculatorPage1.MonthlyExpensesTotal = monthlyEpensesTotal;
            }

            if (!String.IsNullOrWhiteSpace(viewModel.CalculatorPage2.State))
            {
                state = viewModel.CalculatorPage2.State;
                viewModel.CalculatorPage1.State = state;
            }

            if (viewModel.CalculatorPage2.Postcode != null)
            {
                Int32.TryParse(viewModel.CalculatorPage2.Postcode.GetValueOrDefault().ToString(), out postcode);
                viewModel.CalculatorPage1.Postcode = postcode;
            }

            if (viewModel.CalculatorPage2.TotalValue != null)
            {
                Decimal.TryParse(viewModel.CalculatorPage2.TotalValue.GetValueOrDefault().ToString(), out totalValue);
                viewModel.CalculatorPage1.TotalValue = totalValue;
            }

            #endregion This logic is to retain the values from Page 2            

            ViewData["CalculatorViewModel"] = viewModel;
            return View("CalPage1", viewModel);
        }

        /// <summary>
        /// Display Page 2
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        // POST: Calculator/CalPage2      
        public IActionResult Page2(Models.Calculator.CalculatorViewModel viewModel)
        {
            #region Date Of Birth Validation

            DateTime dateOfBirth = new DateTime();
            DateTime expectedDateOfBirth = new DateTime();
            int age = 0;
            if (viewModel.CalculatorPage1.Age != null)
            {
                Int32.TryParse(viewModel.CalculatorPage1.Age.GetValueOrDefault().ToString(), out age);
            }

            if (viewModel.CalculatorPage1.DateOfBirth != null)
            {
                DateTime.TryParse(viewModel.CalculatorPage1.DateOfBirth.ToString(), out dateOfBirth);
            }            
            expectedDateOfBirth = DateTime.Now.AddYears(-age);

            // Check to see if the year of DOB entered is same as the year expected based on the age.
            if (dateOfBirth.Year > expectedDateOfBirth.Year || dateOfBirth.Year < expectedDateOfBirth.Year)
            {
                ModelState.AddModelError("CalculatorPage1.DateOfBirth", "Date of birth is incorrect based on the age. Expected year is " + expectedDateOfBirth.Year);
            }

            #endregion Date Of Birth Validation

            if (ModelState.IsValid)
            {
                List<OccupationRating> occupationRatings = new List<OccupationRating>();
                occupationRatings = GetOccpationRatingList();
                viewModel.OccupationRatings = occupationRatings;
                ViewBag.OccupationRatings = occupationRatings;

                #region This logic is to retain the values from Page 1

                String occupation = "";
                decimal sumInsured = 0;
                decimal monthlyEpensesTotal = 0;
                String state = "";
                int postcode = 0;
                decimal totalValue = 0;

                viewModel.CalculatorPage2 = new CalculatorPage2();
                if (!String.IsNullOrWhiteSpace(viewModel.CalculatorPage1.Occupation))
                {
                    occupation = viewModel.CalculatorPage1.Occupation;
                    viewModel.CalculatorPage2.Occupation = occupation;
                }

                if (viewModel.CalculatorPage1.SumInsured != null)
                {
                    Decimal.TryParse(viewModel.CalculatorPage1.SumInsured.GetValueOrDefault().ToString(), out sumInsured);
                    viewModel.CalculatorPage2.SumInsured = sumInsured;
                }

                if (viewModel.CalculatorPage1.MonthlyExpensesTotal != null)
                {
                    Decimal.TryParse(viewModel.CalculatorPage1.MonthlyExpensesTotal.GetValueOrDefault().ToString(), out monthlyEpensesTotal);
                    viewModel.CalculatorPage2.MonthlyExpensesTotal = monthlyEpensesTotal;
                }

                if (!String.IsNullOrWhiteSpace(viewModel.CalculatorPage1.State))
                {
                    state = viewModel.CalculatorPage1.State;
                    viewModel.CalculatorPage2.State = state;
                }

                if (viewModel.CalculatorPage1.Postcode != null)
                {
                    Int32.TryParse(viewModel.CalculatorPage1.Postcode.GetValueOrDefault().ToString(), out postcode);
                    viewModel.CalculatorPage2.Postcode = postcode;
                }

                if (viewModel.CalculatorPage1.TotalValue != null)
                {
                    Decimal.TryParse(viewModel.CalculatorPage1.TotalValue.GetValueOrDefault().ToString(), out totalValue);
                    viewModel.CalculatorPage2.TotalValue = totalValue;
                }

                #endregion This logic is to retain the values from Page 1

                ViewData["CalculatorViewModel"] = viewModel;
                //TempData["CalculatorViewModel"] = viewModel;

                //return View("CalPage2", viewModel);
                //return RedirectToAction("CalPage2");
                return View("CalPage2", viewModel);
            }
            else
            {
                //TempData["CalculatorViewModel"] = viewModel;
                return View("CalPage1");
            }
        }

        /// <summary>
        /// Calculate TotalValue using Age from Page 1 and SumInsured from Page 2 with the formula provided in the requirement.
        /// Formula = Total Value = (Sum Insured * Occupation Rating Factor) / (100 * 12 * Age)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public IActionResult Calculate(Models.Calculator.CalculatorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Customer customer1 = (Customer)TempData["Customer"];

                // Get the factor from Rating Factor table using Occupation selected by user on Page 2
                List<RatingFactor> ratinFactor = new List<RatingFactor>();
                decimal ratingFactor = (from rf in _context.RatingFactor
                                        where rf.Rating == viewModel.CalculatorPage2.Occupation
                                        select rf.Factor).FirstOrDefault();

                int age = 0;
                if (viewModel.CalculatorPage1.Age != null)
                {
                    // Get integer value from nullable Age object
                    age = viewModel.CalculatorPage1.Age.GetValueOrDefault();
                }

                decimal sumInsured = 0;
                if (viewModel.CalculatorPage2.SumInsured != null)
                {
                    sumInsured = viewModel.CalculatorPage2.SumInsured.GetValueOrDefault(); 
                }

                // Total Value = (Sum Insured * Occupation Rating Factor) / (100 * 12 * Age)
                decimal totalValue = (sumInsured * ratingFactor) / (100 * 12 * age);

                ViewBag.TotalValue = totalValue;
            }

            List<OccupationRating> occupationRatings = new List<OccupationRating>();
            occupationRatings = GetOccpationRatingList();
            viewModel.OccupationRatings = occupationRatings;
            ViewBag.OccupationRatings = occupationRatings;

            //return View(viewModel);
            return View("CalPage2", viewModel);
        }

        private List<OccupationRating> GetOccpationRatingList()
        {
            List<OccupationRating> occupationRatings = new List<OccupationRating>();
            occupationRatings = (from or in _context.OccupationRating
                                 select or).ToList();

            return occupationRatings;
        }
    }
}