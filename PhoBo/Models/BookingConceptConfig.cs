using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class BookingConceptConfig
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Photographer")]
        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        public Concept Concept { get; set; }
        public string DurationConfig { get; set; }
    }
}
