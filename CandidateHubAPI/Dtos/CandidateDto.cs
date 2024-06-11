namespace CandidateHubAPI.Dtos
{
    public class CandidateDto
    {
        /// <summary>
        /// Gets or sets the first name of the candidate.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the candidate.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the candidate. 
        /// Must be a valid international phone number.
        /// </summary>
        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "The phone number is not a valid international phone number.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the candidate.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the start time of the call interval.
        /// Must be a future date.
        /// </summary>
        [FutureDate]
        public DateTimeOffset? CallTimeIntervalStart { get; set; }

        /// <summary>
        /// Gets or sets the end time of the call interval.
        /// Must be greater than the start time.
        /// </summary>
        [DateRange("CallTimeIntervalStart", ErrorMessage = "The end date must be greater than the start date.")]
        public DateTimeOffset? CallTimeIntervalEnd { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn profile URL of the candidate.
        /// Must start with 'https://www.linkedin.com/'.
        /// </summary>
        [LinkedInUrl]
        public string? LinkedInProfileUrl { get; set; }

        /// <summary>
        /// Gets or sets the GitHub profile URL of the candidate.
        /// Must start with 'https://github.com/'.
        /// </summary>
        [GitHubUrl]
        public string? GitHubProfileUrl { get; set; }

        /// <summary>
        /// Gets or sets any additional comments about the candidate.
        /// </summary>
        public string? Comment { get; set; }
    }
}
