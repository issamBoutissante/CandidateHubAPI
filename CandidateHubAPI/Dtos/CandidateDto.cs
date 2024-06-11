namespace CandidateHubAPI.Dtos
{
    public class CandidateDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "The phone number is not a valid international phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [FutureDate]
        public DateTimeOffset? CallTimeIntervalStart { get; set; }

        [DateRange("CallTimeIntervalStart", ErrorMessage = "The end date must be greater than the start date.")]
        public DateTimeOffset? CallTimeIntervalEnd { get; set; }

        [LinkedInUrl]
        public string? LinkedInProfileUrl { get; set; }

        [GitHubUrl]
        public string? GitHubProfileUrl { get; set; }

        public string? Comment { get; set; }
    }
}
