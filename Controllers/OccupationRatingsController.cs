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
    public class OccupationRatingsController : Controller
    {
        private readonly DevProjMainContext _context;

        public OccupationRatingsController(DevProjMainContext context)
        {
            _context = context;
        }

        // GET: OccupationRatings
        public async Task<IActionResult> Index()
        {
            return View(await _context.OccupationRating.ToListAsync());
        }

        // GET: OccupationRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupationRating = await _context.OccupationRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupationRating == null)
            {
                return NotFound();
            }

            return View(occupationRating);
        }

        // GET: OccupationRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OccupationRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Occupation,Rating")] OccupationRating occupationRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(occupationRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupationRating);
        }

        // GET: OccupationRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupationRating = await _context.OccupationRating.FindAsync(id);
            if (occupationRating == null)
            {
                return NotFound();
            }
            return View(occupationRating);
        }

        // POST: OccupationRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Occupation,Rating")] OccupationRating occupationRating)
        {
            if (id != occupationRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occupationRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationRatingExists(occupationRating.Id))
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
            return View(occupationRating);
        }

        // GET: OccupationRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupationRating = await _context.OccupationRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupationRating == null)
            {
                return NotFound();
            }

            return View(occupationRating);
        }

        // POST: OccupationRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var occupationRating = await _context.OccupationRating.FindAsync(id);
            _context.OccupationRating.Remove(occupationRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationRatingExists(int id)
        {
            return _context.OccupationRating.Any(e => e.Id == id);
        }
    }
}
