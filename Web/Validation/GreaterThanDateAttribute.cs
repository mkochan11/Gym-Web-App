using System.ComponentModel.DataAnnotations;

namespace Web.Validation
{
    public class GreaterThanDateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanDateAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
            ErrorMessage = "The {0} must be later than {1}.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            var comparisonValue = (DateTime?)comparisonProperty?.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == null || value == null)
            {
                return ValidationResult.Success;
            }

            var dateValue = (DateTime?)value;

            if (dateValue.HasValue && comparisonValue.HasValue && dateValue <= comparisonValue)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _comparisonProperty));
            }

            return ValidationResult.Success;
        }
    }
}
