using _4AHWII_WebProjekt_MasoodFabian.Models;
using _4AHWII_WebProjekt_MasoodFabian.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace _4AHWII_WebProjekt_MasoodFabian.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbManager _db;

        public HomeController(ILogger<HomeController> logger, DbManager db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var blogPosts = _db.Posts.ToList();
            return View(blogPosts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
