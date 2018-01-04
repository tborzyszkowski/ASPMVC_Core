using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Controllers
{
    public class HumansController : Controller
    {
        private readonly PersonContext _context;

        public HumansController(PersonContext context)
        {
            _context = context;
        }

        // GET: Humans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Human.ToListAsync());
        }

        // GET: Humans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human
                .SingleOrDefaultAsync(m => m.ID == id);
            if (human == null)
            {
                return NotFound();
            }

            return View(human);
        }

        // GET: Humans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Humans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,YoB")] Human human)
        {
            if (ModelState.IsValid)
            {
                _context.Add(human);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(human);
        }

        // GET: Humans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human.SingleOrDefaultAsync(m => m.ID == id);
            if (human == null)
            {
                return NotFound();
            }
            return View(human);
        }

        // POST: Humans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,YoB")] Human human)
        {
            if (id != human.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(human);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HumanExists(human.ID))
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
            return View(human);
        }

        // GET: Humans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human
                .SingleOrDefaultAsync(m => m.ID == id);
            if (human == null)
            {
                return NotFound();
            }

            return View(human);
        }

        // POST: Humans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var human = await _context.Human.SingleOrDefaultAsync(m => m.ID == id);
            _context.Human.Remove(human);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HumanExists(int id)
        {
            return _context.Human.Any(e => e.ID == id);
        }
    }
}
