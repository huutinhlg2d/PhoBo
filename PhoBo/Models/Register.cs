using System;
using System.ComponentModel.DataAnnotations;

namespace PhoBo.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match")]
        public string Repassword { get; set; }

        public DateTime DateOfBirth;
        public UserRole Role { get; set; }

        public User GetUser()
        {
            return new User()
            {
                Name = Name,
                Email = Email,
                Password = Password,
                DateOfBirth = DateOfBirth,
                Role = Role,
            };
        }
    }
}
