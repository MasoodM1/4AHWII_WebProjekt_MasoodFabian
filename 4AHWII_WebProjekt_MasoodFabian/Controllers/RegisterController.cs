using Microsoft.AspNetCore.Mvc;
using _4AHWII_WebProjekt_MasoodFabian.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using _4AHWII_WebProjekt_MasoodFabian.Models.DB;

namespace _4AHWII_WebProjekt_MasoodFabian.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DbManager _dbManager;

        public RegisterController(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            User user = new()
            {
                Geburtsdatum = DateTime.Now
            };
            // Anzeige des Registrierungsformulars
            return View(user);
        }

        static bool isSpecialCharacter(char c)
        {
            string specialCharacters = "!@#$%^&*()-_=+[]{}|;:'\",.<>/?";
            return specialCharacters.Contains(c);
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (user == null)
            {
                return View();
            }
            if (user.Username == null)
            {
                ModelState.AddModelError("Username", "Bitte geben Sie einen Username ein!");
            }
            if (user.Passwort == null || user.Passwort.Trim().Length < 8 ||
               !user.Passwort.Any(char.IsDigit) || !user.Passwort.Any(isSpecialCharacter) ||
               !user.Passwort.Any(char.IsLower) || !user.Passwort.Any(char.IsUpper))
            {
                ModelState.AddModelError("Passwort", "Das Passwort muss mindestens 8 Zeichen lang sein und mindestens eine Zahl, ein Großbuchstabe, ein Kleinbuchstabe und ein Sonderzeichen enthalten!");
            }
            if (user.Email == null || !IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Bitte geben Sie eine gültige Email-Adresse ein!");
            }
            if (user.Geburtsdatum > DateTime.Now)
            {
                ModelState.AddModelError("Geburtsdatum", "Bitte geben Sie ein gültiges Geburtsdatum ein!");
            }
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<User>();
                user.Passwort = passwordHasher.HashPassword(user, user.Passwort);
                _dbManager.Add(user);
                int result = await _dbManager.SaveChangesAsync();
                if (result == 0)
                {
                    return View("Message", new Message()
                    {
                        Title = "Registrierung",
                        MessageText = "Sie konnten leider nicht registriert werden!",
                        Solution = "Bitte versuchen Sie es später erneut!"
                    });
                }
                else
                {
                    return View("Message", new Message()
                    {
                        Title = "Registrierung",
                        MessageText = "Sie wurden erfolgreich registriert!"
                    });
                }
            }
            return View(user);
        }
    }
}
