namespace AgriEnergyPlatform.Models
{
    public class Farmer
    {
        public int FarmerID { get; set; }
        public string FarmerName { get; set; }
        public string FarmerPNumber { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
