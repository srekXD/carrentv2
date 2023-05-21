using System.ComponentModel.DataAnnotations.Schema;

namespace carrent.Data
{
    public enum Status { yes,no }
    public class Car
    {
        public int Id { get; set; }

        public int BrandModelId { get; set; }
        public BrandModel BrandModels { get; set; }
        public int Year { get; set; }

        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public Status Status { get; set; }
        [Column(TypeName="decimal(10,2)")]
        public decimal Price { get; set; }

        public ICollection<Rezervation> Rezervations { get; set; }

        public DateTime RegesterOn { get; set; }

    }
}
