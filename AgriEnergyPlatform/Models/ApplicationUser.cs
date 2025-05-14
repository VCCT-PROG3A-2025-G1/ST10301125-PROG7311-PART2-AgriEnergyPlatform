using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int? FarmerID { get; set; }
        public int? EmployeeID { get; set; }

        public Farmer Farmer { get; set; }
        public Employee Employee { get; set; }
    }
