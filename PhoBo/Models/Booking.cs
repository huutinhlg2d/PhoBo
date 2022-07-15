using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Photographer")]
        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        public Concept Concept { get; set; }
        public DateTime BookingDate { get; set; }
        public float BookingRate { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public float Duration { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public string Note { get; set; }
        [DefaultValue(BookingState.Waiting)]
        public BookingState State { get; set; }
    }
}
