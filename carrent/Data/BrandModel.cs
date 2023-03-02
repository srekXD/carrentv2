namespace carrent.Data
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; }
        
        public DateTime RegesteredOn { get; set; }
    }
}