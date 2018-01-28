using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proj4.Data;
using proj4.Models;
using proj4.Models.MusicViewModels;

namespace proj4.Controllers
{
    public class RatingsController : Controller
    {
        private readonly MusicContext _context;

        public RatingsController(MusicContext context)
        {
            _context = context;
        }

        // GET: BandListeners
        public async Task<IActionResult> Index(int? id
            , string bandID)
        {
            var viewModel = new RatingIndexData();
                viewModel.Listeners = await _context.Listeners
                  .Include(i => i.BandsListeners)
                    .ThenInclude(i => i.Band)
                    //Include(i => i.Listener)
                  //.AsNoTracking()
                  .OrderBy(i => i.Name)
                  .ToListAsync();

            if (id != null) {
                ViewData["ListenerID"] = id.Value;
                Listener listener = viewModel.Listeners.Where(
                    i => i.ListenerID == id.Value).Single();
                viewModel.Bands = listener.BandsListeners.Select(s => s.Band);
            }

            if (bandID != null) {
                ViewData["BandID"] = bandID; //.Value;
                viewModel.Tours = viewModel.Bands.Where(
                    x => x.BandID == bandID).Single().Tours;
            }

            return View(viewModel);
            /*
            var musicContext = _context.BandsListeners.Include(b => b.Band).Include(b => b.Listener);
            return View(await musicContext.ToListAsync());
            */
        }

        // GET: BandListeners/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandListener = await _context.BandsListeners
                .Include(b => b.Band)
                .Include(b => b.Listener)
                .SingleOrDefaultAsync(m => m.BandID == id);
            if (bandListener == null)
            {
                return NotFound();
            }

            return View(bandListener);
        }

        // GET: BandListeners/Create
        public IActionResult Create()
        {
            ViewData["BandID"] = new SelectList(_context.Bands, "BandID", "BandID");
            ViewData["ListenerID"] = new SelectList(_context.Listeners, "ListenerID", "ListenerID");
            return View();
        }

        // POST: BandListeners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListenerID,BandID,Note")] BandListener bandListener)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bandListener);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandID"] = new SelectList(_context.Bands, "BandID", "BandID", bandListener.BandID);
            ViewData["ListenerID"] = new SelectList(_context.Listeners, "ListenerID", "ListenerID", bandListener.ListenerID);
            return View(bandListener);
        }

        // GET: BandListeners/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandListener = await _context.BandsListeners.SingleOrDefaultAsync(m => m.BandID == id);
            if (bandListener == null)
            {
                return NotFound();
            }
            ViewData["BandID"] = new SelectList(_context.Bands, "BandID", "BandID", bandListener.BandID);
            ViewData["ListenerID"] = new SelectList(_context.Listeners, "ListenerID", "ListenerID", bandListener.ListenerID);
            return View(bandListener);
        }

        // POST: BandListeners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ListenerID,BandID,Note")] BandListener bandListener)
        {
            if (id != bandListener.BandID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bandListener);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandListenerExists(bandListener.BandID))
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
            ViewData["BandID"] = new SelectList(_context.Bands, "BandID", "BandID", bandListener.BandID);
            ViewData["ListenerID"] = new SelectList(_context.Listeners, "ListenerID", "ListenerID", bandListener.ListenerID);
            return View(bandListener);
        }

        // GET: BandListeners/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandListener = await _context.BandsListeners
                .Include(b => b.Band)
                .Include(b => b.Listener)
                .SingleOrDefaultAsync(m => m.BandID == id);
            if (bandListener == null)
            {
                return NotFound();
            }

            return View(bandListener);
        }

        // POST: BandListeners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bandListener = await _context.BandsListeners.SingleOrDefaultAsync(m => m.BandID == id);
            _context.BandsListeners.Remove(bandListener);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandListenerExists(string id)
        {
            return _context.BandsListeners.Any(e => e.BandID == id);
        }
    }
}
