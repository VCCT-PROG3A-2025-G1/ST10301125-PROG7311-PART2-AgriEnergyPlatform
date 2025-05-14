namespace AgriEnergyPlatform.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public DateTime ProductionDate { get; set; }

        public int FarmerID { get; set; }
        public Farmer Farmer { get; set; }
    }

}