using Microsoft.AspNetCore.Mvc;
using _4AHWII_WebProjekt_MasoodFabian.Models;
using _4AHWII_WebProjekt_MasoodFabian.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace _4AHWII_WebProjekt_MasoodFabian.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbManager _dbManager;

        public LoginController(DbManager dbManager)
        {
            _dbManager = dbManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User u)
        {
            if (u == null)
            {
                return View();
            }

            var user = await _dbManager.Users
                                       .FirstOrDefaultAsync(user => user.Username == u.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Falsche Benutzerdaten");
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Passwort, u.Passwort);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("userId", user.Id);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Falsche Benutzerdaten");
            return View();
        }
    }
}
