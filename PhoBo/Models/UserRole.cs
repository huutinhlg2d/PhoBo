using System.ComponentModel.DataAnnotations;

namespace PhoBo.Models
{
    public enum UserRole
    {
        [Display]
        Customer,
        Photographer,
        PendingPhotographer,
        Admin
    }
}