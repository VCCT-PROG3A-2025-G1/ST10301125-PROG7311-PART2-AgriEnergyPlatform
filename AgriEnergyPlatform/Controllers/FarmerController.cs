using AgriEnergyPlatform.Data;
using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Ensure you have this for logging if needed

namespace AgriEnergyPlatform.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FarmerController> _logger; // Logger if needed

        public FarmerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<FarmerController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Farmer/Dashboard
        public IActionResult FarmerDashboard()
        {
            var farmerId = User.Identity.Name; // Assuming farmer's ID is stored in their username
            var products = _context.Products.Where(p => p.Farmer.FarmerName == farmerId).ToList();
            return View(products);
        }

        // GET: Farmer/AddProduct
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Farmer/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                // Log to confirm the method is being hit (if you need logging)
                _logger.LogInformation("Product is being added: " + product.ProductName);

                // Get the current logged-in user
                var currentUser = await _userManager.GetUserAsync(User);

                // Find the farmer by UserId
                var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == currentUser.Id);

                if (farmer != null)
                {
                    // Assign the correct FarmerID based on the logged-in farmer
                    product.FarmerID = farmer.FarmerID;
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(FarmerDashboard)); // Redirect to the farmer dashboard after saving
                }

                _logger.LogWarning("Farmer not found for user: " + currentUser.UserName);
                return RedirectToAction(nameof(FarmerDashboard));
            }

            return View(product); // Return the view with validation errors if any
        }
    }
}









/* [Authorize(Roles = "Farmer")]
public class FarmerController : Controller
{
    private readonly ApplicationDbContext _context;

    public FarmerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> FarmerDashboard()
    {
        var user = await _userManager.GetUserAsync(User);

        // Check if the farmer profile is linked with this user
        if (user.FarmerID == null)
        {
            return RedirectToAction("CreateFarmerProfile", "Farmer");
        }

        var farmer = await _context.Farmers
            .FirstOrDefaultAsync(f => f.FarmerID == user.FarmerID.Value);

        ViewBag.UserID = user.FarmerID;

        return View(farmer); // Display Farmer details in the dashboard
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
} */
