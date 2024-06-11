namespace CandidateHubAPI.Tests
{
    public class CandidateServiceTests : TestBase
    {
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
            : base()
        {
            _candidateService = new CandidateService(DbContext, Mapper);
        }

        /// <summary>
        /// Tests that a new candidate is added successfully.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldAddCandidate_WhenNew()
        {
            ClearDatabase();

            var candidate = new Candidate
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            var savedCandidate = await _candidateService.GetCandidateByEmailAsync(candidate.Email);

            Assert.NotNull(savedCandidate);
            Assert.Equal("Issam", savedCandidate.FirstName);
        }

        /// <summary>
        /// Tests that an existing candidate is updated successfully.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldUpdateCandidate_WhenExisting()
        {
            ClearDatabase();

            var candidate = new Candidate
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            candidate.LastName = "Daali";

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            var savedCandidate = await _candidateService.GetCandidateByEmailAsync(candidate.Email);

            Assert.NotNull(savedCandidate);
            Assert.Equal("Daali", savedCandidate.LastName);
        }

        /// <summary>
        /// Tests that all candidates are returned successfully.
        /// </summary>
        [Fact]
        public async Task GetAllCandidates_ShouldReturnAllCandidates()
        {
            ClearDatabase();

            var candidates = new List<Candidate>
            {
                new Candidate { FirstName = "Issam", LastName = "Boutissante", Email = "boutissante.issam.dev@gmail.com", PhoneNumber = "1234567890" },
                new Candidate { FirstName = "Salama", LastName = "Daali", Email = "salama.daali@gmail.com", PhoneNumber = "0987654321" }
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidates[0]);
            await _candidateService.AddOrUpdateCandidateAsync(candidates[1]);

            var result = await _candidateService.GetAllCandidatesAsync();

            Assert.Equal(2, result.Count);
        }
    }
}
