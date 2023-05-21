using Microsoft.AspNetCore.Identity;

namespace carrent.Data
{
    public class Clieunt :IdentityUser
    {
        public string FullName { get; set; }

        public string EGN { get; set; }

        public string Address { get; set; }

        public ICollection<Rezervation> Reservations { get; set; } 

        public DateTime RegesteredOn { get; set; }
    }
}