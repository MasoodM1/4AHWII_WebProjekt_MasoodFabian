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
                if (HttpContext.Session.GetInt32("userId") != null)
                {
                    var userId = HttpContext.Session.GetInt32("userId");
                    var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                    blogPost.User = user;
                }

                blogPost.DatePosted = DateTime.Now;

                _db.Posts.Add(blogPost);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(blogPost);
        }
    }
}