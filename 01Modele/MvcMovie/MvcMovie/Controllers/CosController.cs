using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class CosController : Controller
    {
        private readonly MvcMovieContext _context;

        public CosController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Cos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cos.ToListAsync());
        }

        // GET: Cos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cos = await _context.Cos
                .SingleOrDefaultAsync(m => m.ID == id);
            if (cos == null)
            {
                return NotFound();
            }

            return View(cos);
        }

        // GET: Cos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] Cos cos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cos);
        }

        // GET: Cos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cos = await _context.Cos.SingleOrDefaultAsync(m => m.ID == id);
            if (cos == null)
            {
                return NotFound();
            }
            return View(cos);
        }

        // POST: Cos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] Cos cos)
        {
            if (id != cos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosExists(cos.ID))
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
            return View(cos);
        }

        // GET: Cos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cos = await _context.Cos
                .SingleOrDefaultAsync(m => m.ID == id);
            if (cos == null)
            {
                return NotFound();
            }

            return View(cos);
        }

        // POST: Cos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cos = await _context.Cos.SingleOrDefaultAsync(m => m.ID == id);
            _context.Cos.Remove(cos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosExists(int id)
        {
            return _context.Cos.Any(e => e.ID == id);
        }
    }
}
