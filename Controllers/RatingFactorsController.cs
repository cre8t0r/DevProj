using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevelopmentProject.Data;
using DevelopmentProject.Models;

namespace DevelopmentProject.Controllers
{
    public class RatingFactorsController : Controller
    {
        private readonly DevProjMainContext _context;

        public RatingFactorsController(DevProjMainContext context)
        {
            _context = context;
        }

        // GET: RatingFactors
        public async Task<IActionResult> Index()
        {
            return View(await _context.RatingFactor.ToListAsync());
        }

        // GET: RatingFactors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingFactor = await _context.RatingFactor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingFactor == null)
            {
                return NotFound();
            }

            return View(ratingFactor);
        }

        // GET: RatingFactors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RatingFactors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,Factor")] RatingFactor ratingFactor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingFactor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratingFactor);
        }

        // GET: RatingFactors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingFactor = await _context.RatingFactor.FindAsync(id);
            if (ratingFactor == null)
            {
                return NotFound();
            }
            return View(ratingFactor);
        }

        // POST: RatingFactors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Factor")] RatingFactor ratingFactor)
        {
            if (id != ratingFactor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingFactor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingFactorExists(ratingFactor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ratingFactor);
        }

        // GET: RatingFactors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingFactor = await _context.RatingFactor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingFactor == null)
            {
                return NotFound();
            }

            return View(ratingFactor);
        }

        // POST: RatingFactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingFactor = await _context.RatingFactor.FindAsync(id);
            _context.RatingFactor.Remove(ratingFactor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingFactorExists(int id)
        {
            return _context.RatingFactor.Any(e => e.Id == id);
        }
    }
}
