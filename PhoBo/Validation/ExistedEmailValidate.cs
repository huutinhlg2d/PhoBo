using PhoBo.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhoBo.Validation
{
    public class ExistedEmailValidate : ValidationAttribute
    {
        public ExistedEmailValidate()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            PhoBoContext _context = (PhoBoContext)validationContext
                         .GetService(typeof(PhoBoContext));
            if (_context.User.ToList().Find(u => u.Email == email) != null) return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Email existed";
        }
    }
}
