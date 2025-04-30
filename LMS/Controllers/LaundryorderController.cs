using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Models;

namespace LMS.Controllers
{
    public class LaundryorderController : Controller
    {
        private readonly NeondbContext _context;

        public LaundryorderController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Laundryorder
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Laundryorders.Include(l => l.Customer);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Laundryorder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laundryorder = await _context.Laundryorders
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (laundryorder == null)
            {
                return NotFound();
            }

            return View(laundryorder);
        }

        // GET: Laundryorder/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid");
            return View();
        }

        // POST: Laundryorder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Customerid,Orderdate,Pickupdate,Deliverydate,Status,Totalamount")] Laundryorder laundryorder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laundryorder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", laundryorder.Customerid);
            return View(laundryorder);
        }

        // GET: Laundryorder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laundryorder = await _context.Laundryorders.FindAsync(id);
            if (laundryorder == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", laundryorder.Customerid);
            return View(laundryorder);
        }

        // POST: Laundryorder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Orderid,Customerid,Orderdate,Pickupdate,Deliverydate,Status,Totalamount")] Laundryorder laundryorder)
        {
            if (id != laundryorder.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laundryorder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaundryorderExists(laundryorder.Orderid))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", laundryorder.Customerid);
            return View(laundryorder);
        }

        // GET: Laundryorder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laundryorder = await _context.Laundryorders
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (laundryorder == null)
            {
                return NotFound();
            }

            return View(laundryorder);
        }

        // POST: Laundryorder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laundryorder = await _context.Laundryorders.FindAsync(id);
            if (laundryorder != null)
            {
                _context.Laundryorders.Remove(laundryorder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaundryorderExists(int id)
        {
            return _context.Laundryorders.Any(e => e.Orderid == id);
        }
    }
}
