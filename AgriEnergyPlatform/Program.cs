using Microsoft.Extensions.Configuration;
using AgriEnergyPlatform.Data;
using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnectApp.Data;

namespace AgriEnergyPlatform
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Identity with ApplicationUser and IdentityRole
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add controllers with views (MVC)
            builder.Services.AddControllersWithViews();

            // Add session services
            builder.Services.AddSession();

            // Build the application
            var app = builder.Build();

            // Use HTTPS redirection, static files, routing, etc.
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Set up routing
            app.UseRouting();

            // Set up authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable session middleware
            app.UseSession();

            // Run the database seeding method after the app is built
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DBInitializer.SeedData(context, userManager, roleManager); // This is where the error occurred
            }


            // Map default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the app
            app.Run();
        }
    }
}
