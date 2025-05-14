using AgriEnergyPlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AgriEnergyPlatform.Models;

namespace AgriEnergyConnectApp.Data
{
    public static class DBInitializer
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure DB is created
            context.Database.Migrate();

            // Seed roles
            if (!await roleManager.RoleExistsAsync("Farmer"))
                await roleManager.CreateAsync(new IdentityRole("Farmer"));

            if (!await roleManager.RoleExistsAsync("Employee"))
                await roleManager.CreateAsync(new IdentityRole("Employee"));

            // Seed Application Users if none exist
            if (!context.Users.Any())
            {
                // Create farmer user
                var farmerUser = new ApplicationUser
                {
                    UserName = "farmer@demo.com",
                    Email = "farmer@demo.com",
                    EmailConfirmed = true
                };
                var farmerResult = await userManager.CreateAsync(farmerUser, "Farmer123!");

                // Create employee user
                var employeeUser = new ApplicationUser
                {
                    UserName = "employee@demo.com",
                    Email = "employee@demo.com",
                    EmailConfirmed = true
                };
                var employeeResult = await userManager.CreateAsync(employeeUser, "Employee123!");

                // Assign roles if successful
                if (farmerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser, "Farmer");

                    // Seed Farmer record
                    context.Farmers.Add(new Farmer
                    {
                        FarmerName = "James Johnson",
                        FarmerPNumber = "0726345567",
                        UserId = farmerUser.Id
                    });

                    await context.SaveChangesAsync();
                }

                if (employeeResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");

                    // Seed Employee record
                    context.Employees.Add(new Employee
                    {
                        EmployeeName = "Emma Edwards",
                        EmployeePNumber = "0976353267",
                        EmployeeDepartment = "Vegetables"
                    });

                    await context.SaveChangesAsync();
                }
            }

            // Seed Products if none exist
            if (!context.Products.Any())
            {
                var farmer = context.Farmers.FirstOrDefault(f => f.FarmerName == "James Johnson");
                if (farmer != null)
                {
                    context.Products.Add(new Product
                    {
                        FarmerID = farmer.FarmerID,
                        ProductName = "Tomato",
                        ProductCategory = "Vegetables",
                        ProductionDate = DateTime.Now
                    });

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

