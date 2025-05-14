using System.ComponentModel.DataAnnotations;

namespace AgriEnergyPlatform.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductCategory { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public int FarmerID { get; set; }

        public Farmer Farmer { get; set; }
    }


}