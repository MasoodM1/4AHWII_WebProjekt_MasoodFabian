using _4AHWII_WebProjekt_MasoodFabian.Models;
using _4AHWII_WebProjekt_MasoodFabian.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace _4AHWII_WebProjekt_MasoodFabian.Controllers
{
    public class BlogController : Controller
    {
        private readonly DbManager _db;

        public BlogController(DbManager db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("userId");
                if (userId == null)
                {
                    // Behandeln Sie den Fall, dass keine userId in der Session vorhanden ist.
                    // Möglicherweise möchten Sie den Benutzer zur Anmeldeseite umleiten oder eine Fehlermeldung anzeigen.
                    return RedirectToAction("Login", "Account");
                }

                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    // Behandeln Sie den Fall, dass der Benutzer nicht in der Datenbank gefunden wurde.
                    return NotFound();
                }

                blogPost.User = user;
                blogPost.DatePosted = DateTime.Now;

                _db.Posts.Add(blogPost);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(blogPost);
        }
    }
}