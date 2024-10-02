using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Razorpaycore8.Models;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PetDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public PetDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PetDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetDetails.ToListAsync());
        }

        public async Task<IActionResult> UserPetDetailIndex()
        {
            return View(await _context.PetDetails.ToListAsync());
        }

        // GET: PetDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petDetail = await _context.PetDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petDetail == null)
            {
                return NotFound();
            }

            return View(petDetail);
        }

        public async Task<IActionResult> UserDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petDetail = await _context.PetDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petDetail == null)
            {
                return NotFound();
            }

            return View(petDetail);
        }

        public async Task<IActionResult> UserVerify(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.Ids = _context.MerchantOrders.Select(o => id).ToList();

            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            string guidString = id.ToString(); // Convert GUID to string

            var merchantOrder = await _context.MerchantOrders.FirstOrDefaultAsync(o => o.Email == userEmail);
            var IDname = await _context.MerchantOrders.FirstOrDefaultAsync(o => o.UniqueID == guidString);


            if (merchantOrder != null && IDname != null)
            {
                var petDetail = await _context.PetDetails.FirstOrDefaultAsync(m => m.Id == id);
                return RedirectToAction("UserDetails", petDetail);
            }
            else
            {
                return RedirectToAction("Index", "Payment");
            }
        }

        // GET: PetDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetName,Species,Breed,Age,Gender,Colour,OwnerName,Ownercontact,price")] PetDetail petDetail)
        {
            if (ModelState.IsValid)
            {
                petDetail.Id = Guid.NewGuid();
                _context.Add(petDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petDetail);
        }

        // GET: PetDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petDetail = await _context.PetDetails.FindAsync(id);
            if (petDetail == null)
            {
                return NotFound();
            }
            return View(petDetail);
        }

        // POST: PetDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PetName,Species,Breed,Age,Gender,Colour,OwnerName,Ownercontact,price")] PetDetail petDetail)
        {
            if (id != petDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetDetailExists(petDetail.Id))
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
            return View(petDetail);
        }

        // GET: PetDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petDetail = await _context.PetDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petDetail == null)
            {
                return NotFound();
            }

            return View(petDetail);
        }

        // POST: PetDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var petDetail = await _context.PetDetails.FindAsync(id);
            if (petDetail != null)
            {
                _context.PetDetails.Remove(petDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetDetailExists(Guid id)
        {
            return _context.PetDetails.Any(e => e.Id == id);
        }
    }
}
