using Microsoft.AspNetCore.Identity;

namespace carrent.Data
{
    public class Clieunt :IdentityUser
    {
        public string FullName { get; set; }

        public int EGN { get; set; }

        public string Adres { get; set; }

        public ICollection<Rezervation> Rezerants { get; set; } 

        public DateTime RegesteredOn { get; set; }
    }
}