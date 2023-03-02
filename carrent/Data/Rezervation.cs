namespace carrent.Data
{
    public class Rezervation
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        
        public Car Cars { get; set; }

        public string ClieuntId { get; set; }

        public Clieunt Clieunts { get; set; }

        public DateTime DateOn { get; set; }

        public DateTime DateOff { get; set; }

        public DateTime RegesterOn { get; set; }
    }
}