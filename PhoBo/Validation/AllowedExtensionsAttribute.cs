using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace PhoBo.Validation
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        public string Extensions { get; set; }

        public AllowedExtensionsAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null) return ValidationResult.Success;
            var extension = Path.GetExtension(file.FileName);
            var validateExtension = Extensions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => "." + e.ToLower());
            if (file != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(validateExtension));
                Console.WriteLine(JsonConvert.SerializeObject(extension));
                if (!validateExtension.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Your image's filetype is not valid.";
        }
    }
}
