using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using skultuna.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace skultuna.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // Tar emot en inloggning med POST
        [HttpPost]
        public async Task<ActionResult> Index(User inloggning, string returnUrl = null)
        {
            if (CheckUser(inloggning.Username, inloggning.Password) == true)
            {
                // Allt stämmer, logga in användaren
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, inloggning.Username));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // Skicka användaren vidare till returnUrl om den finns annars till startsidan
                if (returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else // Loginuppgifterna är inte korrekta
            {
                ViewBag.Message = "Felaktigt användarnamn eller lösenord";
                return View();
            }
        }

        // Hämtar data från db och jämför med det vi tagit emot från användarens input
        private bool CheckUser(string username, string password)
        {
            using (CheckLoginContext db = new CheckLoginContext())
            {
                db.Database.EnsureCreated();
                List<User> userList = db.Users.ToList();
                //return View(userList);
                if (username == userList.ElementAt(0).Username && password == userList.ElementAt(0).Password)
                    return true;
                else
                    return false;
            }
        }
        
        // Logga ut användaren
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
