namespace CandidateHubAPI.Validators
{
    public class GitHubUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value as string;
            if (url != null && !url.StartsWith("https://github.com/"))
            {
                return new ValidationResult("The GitHub profile URL must start with 'https://github.com/'.");
            }

            return ValidationResult.Success;
        }
    }
}
