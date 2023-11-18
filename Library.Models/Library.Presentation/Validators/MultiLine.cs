using System.ComponentModel.DataAnnotations;

namespace Library.Presentation.Validators
{
    public class MultiLine : ValidationAttribute
    {
        public int Count0Lines { get; set; }
        public string CustomErr { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return new ValidationResult("Must Insert Value");

                string[] lines = value.ToString().Split(Environment.NewLine);

            if (lines.Length < Count0Lines)
                return new ValidationResult(CustomErr?? $"Must Be More than or Equal {Count0Lines}");

            return ValidationResult.Success; 
        }
    }
}
