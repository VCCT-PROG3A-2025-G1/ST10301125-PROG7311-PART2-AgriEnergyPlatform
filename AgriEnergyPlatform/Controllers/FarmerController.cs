using AgriEnergyPlatform.Data;
using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmerController.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FarmerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> FarmerDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserID = user.FarmerID;
            return View();
        }
       
        // GET: Add a product
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Save a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Get FarmerID from ApplicationUser
                if (user.FarmerID == null)
                {
                    ModelState.AddModelError("", "No farmer profile is linked to this user.");
                    return View(model);
                }

                model.FarmerID = user.FarmerID.Value;
                model.ProductionDate = DateTime.Now;

                _context.Products.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyProducts");
            }
            return View(model);
        }


        // Show products added by the logged-in farmer
        public async Task<IActionResult> MyProducts()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.FarmerID == null)
            {
                return View(new List<Product>());
            }

            var products = await _context.Products
                .Where(p => p.FarmerID == user.FarmerID.Value)
                .ToListAsync();

            return View(products);
        }
    }
}
