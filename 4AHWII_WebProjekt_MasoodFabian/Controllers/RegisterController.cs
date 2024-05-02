using _4AHWII_WebProjekt_MasoodFabian.Models.DB;
using Microsoft.AspNetCore.Mvc;
using _4AHWII_WebProjekt_MasoodFabian.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace _4AHWII_WebProjekt_MasoodFabian.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DbManager _dbManager;
        private readonly PasswordHasher<User> _passwordHasher;

        public RegisterController(DbManager dbManager, PasswordHasher<User> passwordHasher)
        {
            _dbManager = dbManager;
            _passwordHasher = passwordHasher;
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
            if(user == null)
            {
                return View();
            }
            if(user.Username == null)
            {
                ModelState.AddModelError("Username", "Bitte geben Sie einen Username ein!");
            }
            if(user.Passwort == null || user.Passwort.Trim().Length < 8)
            {
                ModelState.AddModelError("Passwort", "Das Passwort muss mindestens 8 Zeichen lang sein!");
            }
            if(user.Passwort == null || !user.Passwort.Any(char.IsDigit))
            {
                ModelState.AddModelError("Passwort", "Das Passwort muss mindestens eine Zahl beinhalten!");
            }
            if(user.Passwort == null || !user.Passwort.Any(isSpecialCharacter)) {
                ModelState.AddModelError("Passwort", "Das Passwort muss mindestens ein Sonderzeichen beinhalten!");
            }
            if(user.Email == null || !IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Bitte geben Sie eine gültige Email-Adresse ein!");
            }
            if(user.Geburtsdatum > DateTime.Now)
            {
                ModelState.AddModelError("Geburtsdatum", "Bitte geben Sie ein gültiges Geburtsdatum ein!");
            }
            if(ModelState.IsValid)
            {
                user.Passwort = _passwordHasher.HashPassword(user, user.Passwort);
                this._dbManager.Add(user);
                int result = await this._dbManager.SaveChangesAsync();
                if(result == 0)
                {
                    return View("Message", new Message()
                    {
                        Title = "Registrierung",
                        MessageText = "Sie konnten leider nicht registriert werden!",
                        Solution = "Bitte versuchen Sie es später erneut!"
                    });
                } else
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
