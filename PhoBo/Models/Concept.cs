using System.ComponentModel.DataAnnotations;

namespace PhoBo.Models
{
    public class Concept
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, ErrorMessage = "The length of Name is less than 50 characters")]
        public string Name { get; set; }
    }
}