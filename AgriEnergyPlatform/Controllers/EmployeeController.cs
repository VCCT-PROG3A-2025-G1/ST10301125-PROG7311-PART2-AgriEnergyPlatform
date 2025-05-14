using AgriEnergyPlatform.Data;
using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyPlatform.Controllers
{
        [Authorize(Roles = "Employee")]
        public class EmployeeController : Controller
        {
            private readonly ApplicationDbContext _context;

            public EmployeeController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Employee/Dashboard
            public IActionResult EmployeeDashboard(string dateFilter, string categoryFilter, string farmerFilter)
            {
                var products = _context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(dateFilter))
                {
                    products = products.Where(p => p.ProductionDate == DateTime.Parse(dateFilter));
                }

                if (!string.IsNullOrEmpty(categoryFilter))
                {
                    products = products.Where(p => p.ProductCategory.Contains(categoryFilter));
                }

                if (!string.IsNullOrEmpty(farmerFilter))
                {
                    products = products.Where(p => p.Farmer.FarmerName.Contains(farmerFilter));
                }

                return View(products.ToList());
            }

            // GET: Employee/AddFarmer
            public IActionResult AddFarmer()
            {
                return View();
            }

            // POST: Employee/AddFarmer
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddFarmer(Farmer farmer)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(farmer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(EmployeeDashboard));
                }
                return View(farmer);
            }
        }


    }
