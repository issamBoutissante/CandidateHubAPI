namespace CandidateHubAPI.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var dateValue = (DateTimeOffset)value;
                if (dateValue <= DateTimeOffset.Now)
                {
                    return new ValidationResult("The start time must be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
