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
    public class FeedbackController : Controller
    {
        private readonly NeondbContext _context;

        public FeedbackController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Feedback
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Feedbacks.Include(f => f.Customer).Include(f => f.Order);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Order)
                .FirstOrDefaultAsync(m => m.Feedbackid == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedback/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid");
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid");
            return View();
        }

        // POST: Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Feedbackid,Customerid,Orderid,Rating,Comments,Feedbackdate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", feedback.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", feedback.Orderid);
            return View(feedback);
        }

        // GET: Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", feedback.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", feedback.Orderid);
            return View(feedback);
        }

        // POST: Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Feedbackid,Customerid,Orderid,Rating,Comments,Feedbackdate")] Feedback feedback)
        {
            if (id != feedback.Feedbackid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Feedbackid))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Customerid", "Customerid", feedback.Customerid);
            ViewData["Orderid"] = new SelectList(_context.Laundryorders, "Orderid", "Orderid", feedback.Orderid);
            return View(feedback);
        }

        // GET: Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Order)
                .FirstOrDefaultAsync(m => m.Feedbackid == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Feedbackid == id);
        }
    }
}
