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
    public class OrderdetailController : Controller
    {
        private readonly NeondbContext _context;

        public OrderdetailController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Orderdetail
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Orderdetails.Include(o => o.Order).Include(o => o.Service);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Orderdetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails
                .Include(o => o.Order)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.Orderdetailid == id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return View(orderdetail);
        }

        // GET: Orderdetail/Create
        public IActionResult Create()
        {
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid");
            ViewData["Serviceid"] = new SelectList(_context.Services, "Serviceid", "Serviceid");
            return View();
        }

        // POST: Orderdetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderdetailid,Orderid,Serviceid,Weightinkg,Subtotal")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewData["Serviceid"] = new SelectList(_context.Services, "Serviceid", "Serviceid", orderdetail.Serviceid);
            return View(orderdetail);
        }

        // GET: Orderdetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails.FindAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewData["Serviceid"] = new SelectList(_context.Services, "Serviceid", "Serviceid", orderdetail.Serviceid);
            return View(orderdetail);
        }

        // POST: Orderdetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Orderdetailid,Orderid,Serviceid,Weightinkg,Subtotal")] Orderdetail orderdetail)
        {
            if (id != orderdetail.Orderdetailid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderdetailExists(orderdetail.Orderdetailid))
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
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewData["Serviceid"] = new SelectList(_context.Services, "Serviceid", "Serviceid", orderdetail.Serviceid);
            return View(orderdetail);
        }

        // GET: Orderdetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails
                .Include(o => o.Order)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.Orderdetailid == id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return View(orderdetail);
        }

        // POST: Orderdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderdetail = await _context.Orderdetails.FindAsync(id);
            if (orderdetail != null)
            {
                _context.Orderdetails.Remove(orderdetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderdetailExists(int id)
        {
            return _context.Orderdetails.Any(e => e.Orderdetailid == id);
        }
    }
}
