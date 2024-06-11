namespace CandidateHubAPI.Validators
{
    /// <summary>
    /// Validates that the LinkedIn URL is valid.
    /// </summary>
    public class LinkedInUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value as string;
            if (url != null && !url.StartsWith("https://www.linkedin.com/"))
            {
                return new ValidationResult("The LinkedIn profile URL must start with 'https://www.linkedin.com/'.");
            }

            return ValidationResult.Success;
        }
    }
}
