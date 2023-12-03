using Microsoft.AspNetCore.Mvc;
using ShortUrl.Models;
using ShortUrl.Services;
using System;
using System.Diagnostics;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        public IActionResult Index()
        {
            using (_context = new())
            {
                MainPageViewModel viewModel = new MainPageViewModel
                {
                    urlModels = _context.url.ToList()
                };
                return View(viewModel);
            }
        }
        [HttpPost]
        public IActionResult Index(MainPageViewModel mainpageModel)
        {
            using (_context = new())
            {
                mainpageModel.urlModels = _context.url.ToList();
                if (mainpageModel.addUrlsModel.LongUrl == null) return View(mainpageModel);
                mainpageModel.addUrlsModel.ShortUrl = UrlsService.ShortenUrl(mainpageModel.addUrlsModel.LongUrl);
                if (_context.url.Where(p => p.LongUrl == mainpageModel.addUrlsModel.LongUrl).Count() >= 1)
                    return View(mainpageModel);
                _context.Add(new UrlModel
                {
                    LongUrl = mainpageModel.addUrlsModel.LongUrl,
                    ShortUrl = mainpageModel.addUrlsModel.ShortUrl,
                    dateCreated = DateTime.Today,
                    CountOfClick = 0
                });
                _context.SaveChanges();
                mainpageModel.urlModels = _context.url.ToList();
                return View(mainpageModel);
            }

        }
        [HttpPost]
        public IActionResult DeleteUrl(int id)
        {
            using (_context = new())
            {
                _context.url.Remove(_context.url.Where(p => p.Id == id).FirstOrDefault());
                _context.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
        public IActionResult UpdatePageUrl(int id)
        {
            using (_context = new())
            {
                UrlModel model = _context.url.Where(p => p.Id == id).FirstOrDefault();
                model.ShortUrl = model.ShortUrl.Substring(model.ShortUrl.Length - 8);
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult UpdateUrl(UrlModel model, int id)
        {
            using (_context = new())
            {
                model.Id = id;
                model.dateCreated = DateTime.Today;
                model.ShortUrl = "https://localhost:7292/" + model.ShortUrl;
                _context.url.Update(model);
                _context.SaveChanges();
                return Redirect("/Home/Index");
            }
        }
    }
}