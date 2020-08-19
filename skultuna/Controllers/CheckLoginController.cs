using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skultuna.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    // Gjorde denna för att lägga till en användare till databasen som jag sedan kollar login emot
namespace skultuna.Controllers
{
    public class CheckLoginController : Controller
    {
        // Hämtar data ur db och retunerar det
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (CheckLoginContext db = new CheckLoginContext())
            {
                db.Database.EnsureCreated();
                List<User> userList = db.Users.ToList();
                return View(userList);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // Tar emot en användare med POST och lägger till det i db
        [HttpPost]
        public IActionResult Create(User user)
        {
            using (CheckLoginContext db = new CheckLoginContext())
            {
                db.Database.EnsureCreated();
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}