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
    public class VeterinaryDrsController : Controller
    {
        private readonly AppDbContext _context;

        public VeterinaryDrsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VeterinaryDrs
        public async Task<IActionResult> Index()
        {
            return View(await _context.VeterinaryDrs.ToListAsync());
        }

        // GET: VeterinaryDrs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryDr = await _context.VeterinaryDrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinaryDr == null)
            {
                return NotFound();
            }

            return View(veterinaryDr);
        }

        // GET: VeterinaryDrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VeterinaryDrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Designation,EmailAddress,ClinicAddress,WorkingHours,Contact")] VeterinaryDr veterinaryDr)
        {
            if (ModelState.IsValid)
            {
                veterinaryDr.Id = Guid.NewGuid();
                _context.Add(veterinaryDr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinaryDr);
        }

        // GET: VeterinaryDrs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryDr = await _context.VeterinaryDrs.FindAsync(id);
            if (veterinaryDr == null)
            {
                return NotFound();
            }
            return View(veterinaryDr);
        }

        // POST: VeterinaryDrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FullName,Designation,EmailAddress,ClinicAddress,WorkingHours,Contact")] VeterinaryDr veterinaryDr)
        {
            if (id != veterinaryDr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinaryDr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinaryDrExists(veterinaryDr.Id))
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
            return View(veterinaryDr);
        }

        // GET: VeterinaryDrs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryDr = await _context.VeterinaryDrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinaryDr == null)
            {
                return NotFound();
            }

            return View(veterinaryDr);
        }

        // POST: VeterinaryDrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var veterinaryDr = await _context.VeterinaryDrs.FindAsync(id);
            if (veterinaryDr != null)
            {
                _context.VeterinaryDrs.Remove(veterinaryDr);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinaryDrExists(Guid id)
        {
            return _context.VeterinaryDrs.Any(e => e.Id == id);
        }
    }
}
