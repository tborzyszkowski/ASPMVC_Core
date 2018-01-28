using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using proj4.Data;
using proj4.Models;
using proj4.Authorization;

namespace proj4.Controllers {
    public class BandsController : Controller {
        private readonly MusicContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManger;

        public BandsController(MusicContext context
            , IAuthorizationService authorizationService
            , UserManager<ApplicationUser> userManager) {

            _context = context;
            _userManger = userManager;
            _authorizationService = authorizationService;
        }

        // GET: Bands
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page) {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "FormationDate" ? "date_desc" : "FormationDate";

            if (searchString != null) {
                page = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var bands = from b in _context.Bands
                        select b;
            if (!String.IsNullOrEmpty(searchString)) {
                bands = bands.Where(b => b.BandID.Contains(searchString));
            }
            switch (sortOrder) {
                case "name_desc":
                    bands = bands.OrderByDescending(b => b.BandID);
                    break;
                case "FormationDate":
                    bands = bands.OrderBy(b => b.FormationDate);
                    break;
                case "date_desc":
                    bands = bands.OrderByDescending(b => b.FormationDate);
                    break;
                default:
                    bands = bands.OrderBy(b => b.BandID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Band>.CreateAsync(bands.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Bands/Details/5
        [Route("MoreInfo")]
        public async Task<IActionResult> Details(string id) {
            if (id == null) {
                return NotFound();
            }

            var band = await _context.Bands
                .Include(b => b.BandsListeners)
                    .ThenInclude(bl => bl.Listener)
                //.Include(t => t.Tours)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BandID == id);

            if (band == null) {
                return NotFound();
            }

            return View(band);
        }

        // GET: Bands/Create
        [Authorize(Roles = "BandManager")]
        public IActionResult Create() {
            return View();
        }

        // POST: Bands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BandID,City,FormationDate,Genre")] Band band) {
            try {
                if (ModelState.IsValid) {
                    _context.Add(band);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (Exception ex) {
                ModelState.AddModelError("", "Unable to save changes. Try again or contact me.");
                //throw;
            }
            return View(band);
        }

        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var band = await _context.Bands.SingleOrDefaultAsync(m => m.BandID == id);
            if (band == null) {
                return NotFound();
            }
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BandID,City,FormationDate,Genre")] Band band) {
            if (id != band.BandID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(band);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BandExists(band.BandID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(band);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var band = await _context.Bands
                .SingleOrDefaultAsync(m => m.BandID == id);
            if (band == null) {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User
                , band
                , BandOperations.Delete);

            if (!isAuthorized.Succeeded) {
                return new ChallengeResult();
            }

            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var band = await _context.Bands.SingleOrDefaultAsync(m => m.BandID == id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User
                , band
                , BandOperations.Delete);

            if (!isAuthorized.Succeeded) {
                return new ChallengeResult();
            }

            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandExists(string id) {
            return _context.Bands.Any(e => e.BandID == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyBandID(string bandID, string city) {
            var foundBand = _context.Bands.Find(bandID);
            if (foundBand != null && foundBand.City == city && !string.IsNullOrEmpty(foundBand.City)) {
                return Json(data: $"Band with name {bandID} from {city} already exists");
            }
            return Json(data: true);
        }
    }
}
