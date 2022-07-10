using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class Customer : User
    {          
        [InverseProperty("Customer")]
        public ICollection<Booking> UserBookings;
    }
}
