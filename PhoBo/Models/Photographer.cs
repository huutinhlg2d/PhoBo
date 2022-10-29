using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class Photographer : User
    {
        public float Rate { get; set; }

        [InverseProperty("Photographer")]
        public ICollection<Booking> PhotographerBookings { get; set; }

        public ICollection<BookingConceptConfig> BookingConceptConfigs { get; set; }

        public Photographer()
        {
        }

        public Photographer(User user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.Password;
            this.DateOfBirth = user.DateOfBirth;
            this.Role = user.Role;
            this.AvatarUrl = user.AvatarUrl;
            this.Rate = 0;
        }
    }
}
