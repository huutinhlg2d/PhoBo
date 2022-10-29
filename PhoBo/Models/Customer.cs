using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class Customer : User
    {          


        [InverseProperty("Customer")]
        public ICollection<Booking> UserBookings;

        public Customer()
        {
        }

        public Customer(User user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.Password;
            this.DateOfBirth = user.DateOfBirth;
            this.Role = user.Role;
            this.AvatarUrl = user.AvatarUrl;
        }
    }
}
