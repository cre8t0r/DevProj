using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using DevelopmentProject.Data;
using DevelopmentProject.Models;
using DevelopmentProject.Models.Calculator;
using DevelopmentProject.BusinessLogic;

namespace DevelopmentProject.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly DevProjMainContext _context;
        private readonly ICustomerRepository _customerRepository;

        public CalculatorController(DevProjMainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// First page of the calculator
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Page1(int customerID)
        {
            if (customerID > 0)
            {
                // If the customer ID is passed in, use it to retrieve the customer information
                var customer = _context.Customer.FirstOrDefault(m => m.Id == customerID);
                if (customer == null)
                {
                    return NotFound();
                }

                CalculatorViewModel calViewModel = new CalculatorViewModel();
                calViewModel.CalculatorPage1 = new CalculatorPage1() { Name = customer.Name, Age = customer.Age, DateOfBirth = customer.DateOfBirth.ToString("dd/MM/yyyy") };
                //calViewModel.CalculatorPage1.Name = customer.Name;
                //calViewModel.CalculatorPage1.Age = customer.Age;
                //calViewModel.CalculatorPage1.DateOfBirth = customer.DateOfBirth.ToString("dd/MM/yyyy");

                calViewModel.CalculatorPage2 = new CalculatorPage2()
                {
                    Occupation = customer.Occupation,
                    SumInsured = customer.SumInsured,
                    MonthlyExpensesTotal = customer.MonthlyExpensesTotal,
                    State = customer.State,
                    Postcode = customer.Postcode
                };
                //calViewModel.CalculatorPage2.Occupation = customer.Occupation;
                //calViewModel.CalculatorPage2.SumInsured = customer.SumInsured;
                //calViewModel.CalculatorPage2.MonthlyExpensesTotal = customer.MonthlyExpensesTotal;
                //calViewModel.CalculatorPage2.State = customer.State;
                //calViewModel.CalculatorPage2.Postcode = customer.Postcode;

                return View("CalPage1", calViewModel);
            }
            else 
            {
                return View("CalPage1");
            }          
        }

        //[HttpGet]
        //// POST: Calculator/CalPage2       
        //public IActionResult Page2()
        //{
        //    return View("CalPage2");
        //}

        [HttpGet]
        // POST: Calculator/CalPage2       
        public IActionResult Page2(int customerID)
        {
            if (customerID > 0)
            {
                var customer = _context.Customer.FirstOrDefault(m => m.Id == customerID);
                if (customer == null)
                {
                    return NotFound();
                }

                CalculatorViewModel calViewModel = new CalculatorViewModel();
                calViewModel.CalculatorPage1 = new CalculatorPage1() { Name = customer.Name, Age = customer.Age, DateOfBirth = customer.DateOfBirth.ToString("dd/MM/yyyy") };

                var occupationRating = _context.OccupationRating.FirstOrDefault(m => m.Rating == customer.Occupation);

                calViewModel.CalculatorPage2 = new CalculatorPage2()
                {
                    Occupation = occupationRating.Occupation,
                    SumInsured = customer.SumInsured,
                    MonthlyExpensesTotal = customer.MonthlyExpensesTotal,
                    State = customer.State,
                    Postcode = customer.Postcode
                };

                List<OccupationRating> occupationRatings = new List<OccupationRating>();
                occupationRatings = GetOccpationRatingList();
                calViewModel.OccupationRatings = occupationRatings;
                ViewBag.OccupationRatings = occupationRatings;

                return View("CalPage2", calViewModel);
            }
            else
            {
                return View("CalPage2");
            }            
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

                #region Save Customer (For future use)

                Customer customer = new Customer();

                DateTime dob = new DateTime();
                DateTime.TryParse(viewModel.CalculatorPage1.DateOfBirth.ToString(), out dob);

                customer.Name = viewModel.CalculatorPage1.Name;
                customer.Age = viewModel.CalculatorPage1.Age.GetValueOrDefault();
                customer.DateOfBirth = dob;
                customer.Occupation = viewModel.CalculatorPage2.Occupation;
                customer.SumInsured = viewModel.CalculatorPage2.SumInsured.GetValueOrDefault();
                customer.MonthlyExpensesTotal = viewModel.CalculatorPage2.MonthlyExpensesTotal.GetValueOrDefault();
                customer.State = viewModel.CalculatorPage2.State;
                customer.Postcode = viewModel.CalculatorPage2.Postcode.GetValueOrDefault();

                // Save customer details in the database
                _context.Customer.Add(customer);
                _context.SaveChanges();                

                #endregion Save Customer (For future use)

                // Get the factor from Rating Factor table using Occupation selected by user on Page 2
                List<RatingFactor> ratinFactor = new List<RatingFactor>();
                decimal ratingFactor = (from rf in _context.RatingFactor
                                        where rf.Rating == viewModel.CalculatorPage2.Occupation
                                        select rf.Factor).FirstOrDefault();

                // Calculate Total Value (Total Value = (Sum Insured * Occupation Rating Factor) / (100 * 12 * Age))
                decimal totalValue = CalculationLogic.CalculateTotalValue(customer: customer, ratingFactor: ratingFactor);

                ViewBag.TotalValue = totalValue;
            }

            List<OccupationRating> occupationRatings = new List<OccupationRating>();
            occupationRatings = GetOccpationRatingList();
            viewModel.OccupationRatings = occupationRatings;
            ViewBag.OccupationRatings = occupationRatings;
            
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