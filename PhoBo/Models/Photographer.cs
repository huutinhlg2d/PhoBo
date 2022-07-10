using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class Photographer : User
    {
        public float Rate { get; set; }

        [InverseProperty("Photographer")]
        public ICollection<Booking> PhotographerBookings;
    }
}
