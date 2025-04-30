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
    public class ComplaintController : Controller
    {
        private readonly NeondbContext _context;

        public ComplaintController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Complaint
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Complaints.Include(c => c.Customer).Include(c => c.Order);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Complaint/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.Customer)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.Complaintid == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaint/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid");
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid");
            return View();
        }

        // POST: Complaint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Complaintid,Customerid,Orderid,Description,Complaintdate,Status")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", complaint.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", complaint.Orderid);
            return View(complaint);
        }

        // GET: Complaint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", complaint.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", complaint.Orderid);
            return View(complaint);
        }

        // POST: Complaint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Complaintid,Customerid,Orderid,Description,Complaintdate,Status")] Complaint complaint)
        {
            if (id != complaint.Complaintid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Complaintid))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", complaint.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", complaint.Orderid);
            return View(complaint);
        }

        // GET: Complaint/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.Customer)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.Complaintid == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Complaintid == id);
        }
    }
}
