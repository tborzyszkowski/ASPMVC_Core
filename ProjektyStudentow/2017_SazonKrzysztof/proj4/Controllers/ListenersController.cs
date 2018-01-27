using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proj4.Data;
using proj4.Models;
using proj4.Models.MusicViewModels;

namespace proj4.Controllers {
    public class ListenersController : Controller {
        private readonly MusicContext _context;
        const string SessionKeyLastListener = "_LastListener";

        public ListenersController(MusicContext context) {
            _context = context;
        }

        // GET: Listeners
        public async Task<IActionResult> Index(int? id
            , string bandID) {

            var viewModel = new RatingIndexData();
            viewModel.Listeners = await _context.Listeners
                  .Include(i => i.BandsListeners)
                    .ThenInclude(i => i.Band)
                  .OrderBy(i => i.Name)
                  .ToListAsync();

            if (id != null) {
                ViewData["ListenerID"] = id.Value;
                Listener listener = viewModel.Listeners.Where(
                    i => i.ListenerID == id.Value).Single();
                viewModel.BandsListeners = listener.BandsListeners;
            }

            if (bandID != "" && bandID != null) {
                ViewData["BandID"] = bandID;

                var selectedBand = viewModel.BandsListeners.Where(b => b.BandID == bandID).Single().Band;
                await _context.Entry(selectedBand).Collection(b => b.Tours).LoadAsync();
                foreach (Tour t in selectedBand.Tours) {
                    await _context.Entry(t).Reference(i => i.Band).LoadAsync();
                }
                viewModel.Tours = selectedBand.Tours;
            }
            return View(viewModel);
        }

        // GET: Listeners/Details/5
        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                return NotFound();
            }

            var listener = await _context.Listeners
                .SingleOrDefaultAsync(m => m.ListenerID == id);
            if (listener == null) {
                return NotFound();
            }
            HttpContext.Session.SetString(SessionKeyLastListener, listener.Name);
            return View(listener);
        }

        // GET: Listeners/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Listeners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateOfBirth,EmailAddress")] Listener listener) //ListenerID
        {
            try {
                if (ModelState.IsValid) {
                    _context.Add(listener);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception ex) {
                ModelState.AddModelError("", "Unable to save changes. Try again or contact me.");
                //throw;
            }
            return View(listener);
        }

        // GET: Listeners/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var listener = await _context.Listeners.SingleOrDefaultAsync(m => m.ListenerID == id);
            if (listener == null) {
                return NotFound();
            }
            return View(listener);
        }

        // POST: Listeners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) { //, [Bind("ListenerID,Name,DateOfBirth,EmailAddress")] Listener listener) {
            if (id == null) {
                return NotFound();
            }

            var listenerToUpdate = await _context.Listeners.SingleOrDefaultAsync(l => l.ListenerID == id);
            if (await TryUpdateModelAsync<Listener>(
                listenerToUpdate,
                "",
                l => l.Name, l => l.DateOfBirth, l => l.EmailAddress)) {
                try {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch (DbUpdateException ex) {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }

            return View(listenerToUpdate);
        }

        // GET: Listeners/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false) {
            if (id == null) {
                return NotFound();
            }

            var listener = await _context.Listeners
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ListenerID == id);

            if (listener == null) {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault()) {
                ViewData["ErrorMessage"] = "Delete failed";
            }
            return View(listener);
        }

        // POST: Listeners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var listener = await _context.Listeners
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ListenerID == id);

            if (listener == null) {
                return RedirectToAction(nameof(Index));
            }

            try {
                _context.Listeners.Remove(listener);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (DbUpdateException ex) {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ListenerExists(int id) {
            return _context.Listeners.Any(e => e.ListenerID == id);
        }
    }
}
