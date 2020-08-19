using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skultuna.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Skultuna.Controllers
{
    public class ProductController : Controller
    {
        // Hämtar data ur db och retunerar det
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (ProductContext db = new ProductContext())
            {
                db.Database.EnsureCreated();
                List<Product> productList = db.Products.ToList();
                return View(productList);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // Tar emot en produkt med POST och lägger till i db
        [HttpPost]
        public IActionResult Create(Product product)
        {
            using (ProductContext db = new ProductContext())
            {
                db.Database.EnsureCreated();
                db.Products.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}