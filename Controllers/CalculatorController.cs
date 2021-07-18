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
            //Models.Calculator.CalculatorViewModel viewModel = new CalculatorViewModel();
            //viewModel = GetCalculatorViewModel(); 

            if (ModelState.IsValid)
            {
                List<OccupationRating> occupationRatings = new List<OccupationRating>();
                occupationRatings = GetOccpationRatingList();
                viewModel.OccupationRatings = occupationRatings;
                ViewBag.OccupationRatings = occupationRatings;

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

        ///// <summary>
        ///// Load the occupation rating list for the dropdown
        ///// </summary>
        ////private void LoadOccpationRatingList(CalculatorViewModel calculatorViewModel)
        //private void LoadOccupationRatingList()
        //{
        //    CalculatorViewModel viewModel = new CalculatorViewModel();
        //    viewModel = GetCalculatorViewModel();

        //    List<OccupationRating> occupationRatings = new List<OccupationRating>();
        //    occupationRatings = this.GetOccpationRatingList();

        //    // TODO: For future use
        //    //occupationRatings.Insert(0, new OccupationRating { Occupation = "- Select -", Rating = "" });

        //    viewModel.OccupationRatings = new List<OccupationRating>();
        //    viewModel.OccupationRatings = occupationRatings;


        //    ViewBag.OccupationRatings = viewModel.OccupationRatings;
        //}

        //// Get CalculatorViewModel from ViewData object
        ////private CalculatorViewModel GetCalculatorViewModel(CalculatorViewModel calculatorViewModel)
        //private CalculatorViewModel GetCalculatorViewModel()
        //{
        //    CalculatorViewModel viewModel = new CalculatorViewModel();

        //    if (ViewData["CalculatorViewModel"] != null)
        //    {
        //        if (ViewData["CalculatorViewModel"] is CalculatorViewModel)
        //        {
        //            viewModel = (CalculatorViewModel)ViewData["CalculatorViewModel"];
        //        }
        //    }

        //    return viewModel;
        //}
    }
}