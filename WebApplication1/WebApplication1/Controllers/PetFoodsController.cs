using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PetFoodsController : Controller
    {
        private readonly AppDbContext _context;

        public PetFoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PetFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetFoods.ToListAsync());
        }

        // GET: PetFoods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petFood == null)
            {
                return NotFound();
            }

            return View(petFood);
        }

        // GET: PetFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Brand,Species,Calories,PackageSize,Prize,ExpDate,Certifications")] PetFood petFood)
        {
            if (ModelState.IsValid)
            {
                petFood.Id = Guid.NewGuid();
                _context.Add(petFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petFood);
        }

        // GET: PetFoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods.FindAsync(id);
            if (petFood == null)
            {
                return NotFound();
            }
            return View(petFood);
        }

        // POST: PetFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductName,Brand,Species,Calories,PackageSize,Prize,ExpDate,Certifications")] PetFood petFood)
        {
            if (id != petFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetFoodExists(petFood.Id))
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
            return View(petFood);
        }

        // GET: PetFoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petFood == null)
            {
                return NotFound();
            }

            return View(petFood);
        }

        // POST: PetFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var petFood = await _context.PetFoods.FindAsync(id);
            if (petFood != null)
            {
                _context.PetFoods.Remove(petFood);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetFoodExists(Guid id)
        {
            return _context.PetFoods.Any(e => e.Id == id);
        }
    }
}
