using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoBo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of password is from 3 to 20 characters")]
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
    }
}
