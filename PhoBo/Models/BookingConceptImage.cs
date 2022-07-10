using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class BookingConceptImage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BookingConceptConfig")]
        public int ConfigId { get; set; }
        public BookingConceptConfig Config { get; set; }
        public string ImageUrl { get; set; }
    }
}
