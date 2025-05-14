namespace AgriEnergyPlatform.Models
{
    public class Farmer
    {
        public int FarmerID { get; set; }
        public string FarmerName { get; set; }
        public string FarmerPNumber { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
