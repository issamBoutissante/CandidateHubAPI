namespace CandidateHubAPI.Models
{
    [Index(nameof(Email),IsUnique = true)]
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTimeOffset? CallTimeIntervalStart { get; set; }
        public DateTimeOffset? CallTimeIntervalEnd { get; set; }

        [Url]
        public string? LinkedInProfileUrl { get; set; }

        [Url]
        public string? GitHubProfileUrl { get; set; }

        public string? Comment { get; set; }
    }
}
