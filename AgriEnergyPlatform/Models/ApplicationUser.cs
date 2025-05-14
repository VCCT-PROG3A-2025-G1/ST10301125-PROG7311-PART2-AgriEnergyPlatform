using AgriEnergyPlatform.Models;
using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string ?FarmerName { get; set; }
    public string ?EmployeeName { get; set; }
    public string ?Role { get; set; }
        public int? FarmerID { get; set; }
    public int? EmployeeID { get; set; }

    public string? FullName { get; set; }

        public Farmer Farmer { get; set; }
        public Employee Employee { get; set; }
    }
