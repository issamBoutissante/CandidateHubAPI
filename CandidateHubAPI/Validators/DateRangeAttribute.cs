namespace CandidateHubAPI.Validators
{
    /// <summary>
    /// Validates that the end date is greater than the start date.
    /// </summary>
    public class DateRangeAttribute : ValidationAttribute
    {
        public string StartDatePropertyName { get; }

        public DateRangeAttribute(string startDatePropertyName)
        {
            StartDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(StartDatePropertyName);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {StartDatePropertyName}");
            }

            var startDate = (DateTimeOffset?)startDateProperty.GetValue(validationContext.ObjectInstance);
            var endDate = (DateTimeOffset?)value;

            if (startDate.HasValue && endDate.HasValue && endDate <= startDate)
            {
                return new ValidationResult("The end date must be greater than the start date.");
            }

            return ValidationResult.Success;
        }
    }
}
