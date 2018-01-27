using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proj4.Models;
using proj4.Models.MusicViewModels;
using proj4.Data;
using proj4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace proj4.Controllers {
    public class HomeController : Controller {
        private readonly MusicContext _context;
        //const string SessionKeyName = "_Name";
        //const string SessionKeyBasketItemsCount = "_ItemsCount";
        const string SessionKeyLastListener = "_LastListener";
        public AppData _appData;
        //private readonly IDistributedCache _distributedCache;

        public HomeController(MusicContext context
            , AppData appData
            //, IDistributedCache distributedCache
            ) {

            //_distributedCache = distributedCache;
            _context = context;
            _appData = appData;
        }

        public IActionResult Index() {
            //_distributedCache.SetString(SessionKeyName, "Vaclav");
            //HttpContext.Session.SetString(SessionKeyName, "Pumpernikel");
            //HttpContext.Session.SetInt32(SessionKeyBasketItemsCount, 2);
            _appData.MainPageViews++;
            ViewData["MainPageViews"] = _appData.MainPageViews;
            ViewData["LastListener"] = HttpContext.Session.GetString(SessionKeyLastListener);
            return View();
        }

        public async Task<IActionResult> About() {
            ViewData["Message"] = "Your application description page.";
            IQueryable<BandGenreGroup> data =
                from band in _context.Bands
                group band by band.Genre
                into genreGroup
                select new BandGenreGroup() {
                    Genre = genreGroup.Key,
                    GenreCount = genreGroup.Count()
                };

            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";
            //var name = HttpContext.Session.GetString(SessionKeyName);
            //var basketCount = HttpContext.Session.GetInt32(SessionKeyBasketItemsCount);
            //ViewData["Name"] = name;
            //ViewData["BasketCount"] = basketCount;
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
