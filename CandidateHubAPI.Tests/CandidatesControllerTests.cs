namespace CandidateHubAPI.Tests
{
    public class CandidatesControllerTests : TestBase
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateService> _mockService;
        private readonly ILogger<CandidatesController> _logger;

        public CandidatesControllerTests()
            : base()
        {
            _mockService = new Mock<ICandidateService>();

            // Initialize ILogger<CandidatesController>
            _logger = LoggerFactory.CreateLogger<CandidatesController>();

            _controller = new CandidatesController(_mockService.Object, _logger);
        }

        /// <summary>
        /// Tests that a valid candidate model returns an OK result.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnOk_WhenValidModel()
        {
            var candidateDto = new CandidateDto
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

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

            _mockService.Setup(service => service.AddOrUpdateCandidateAsync(It.IsAny<CandidateDto>()))
                        .ReturnsAsync(candidate);

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal("Issam", returnValue.FirstName);
            Assert.Equal("Boutissante", returnValue.LastName);
            Assert.Equal("boutissante.issam.dev@gmail.com", returnValue.Email);
        }

        /// <summary>
        /// Tests that a valid existing candidate model returns an OK result.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnOk_WhenExisting()
        {
            var candidateDto = new CandidateDto
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            var existingCandidate = new Candidate
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            _mockService.Setup(service => service.GetCandidateByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync(existingCandidate);

            _mockService.Setup(service => service.AddOrUpdateCandidateAsync(It.IsAny<CandidateDto>()))
                        .ReturnsAsync(existingCandidate);

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal("Issam", returnValue.FirstName);
            Assert.Equal("Boutissante", returnValue.LastName);
            Assert.Equal("boutissante.issam.dev@gmail.com", returnValue.Email);
        }

        /// <summary>
        /// Tests that the GetAllCandidates method returns an OK result with a list of candidate DTOs.
        /// </summary>
        [Fact]
        public async Task GetAllCandidates_ShouldReturnOk_WithListOfCandidates()
        {
            var candidateDtos = new List<CandidateDto>
            {
                new CandidateDto { FirstName = "Issam", LastName = "Boutissante", Email = "boutissante.issam.dev@gmail.com", PhoneNumber = "1234567890" },
                new CandidateDto { FirstName = "Salama", LastName = "Daali", Email = "salama.daali@gmail.com", PhoneNumber = "0987654321" }
            };

            _mockService.Setup(service => service.GetAllCandidatesAsync())
                        .ReturnsAsync(candidateDtos);

            var result = await _controller.GetAllCandidates();

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var candidatesList = Assert.IsType<List<CandidateDto>>(okResult.Value);
            Assert.Equal(2, candidatesList.Count);
        }
    }
}
