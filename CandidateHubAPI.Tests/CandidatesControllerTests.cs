namespace CandidateHubAPI.Tests
{
    public class CandidatesControllerTests : TestBase
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateService> _mockService;

        public CandidatesControllerTests()
            : base()
        {
            _mockService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockService.Object, Mapper);
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

            _mockService.Setup(service => service.AddOrUpdateCandidateAsync(It.IsAny<Candidate>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal("Issam", returnValue.FirstName);
            Assert.Equal("Boutissante", returnValue.LastName);
            Assert.Equal("boutissante.issam.dev@gmail.com", returnValue.Email);
        }

        /// <summary>
        /// Tests that the GetAllCandidates method returns an OK result with a list of candidates.
        /// </summary>
        [Fact]
        public async Task GetAllCandidates_ShouldReturnOk_WithListOfCandidates()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { FirstName = "Issam", LastName = "Boutissante", Email = "boutissante.issam.dev@gmail.com" },
                new Candidate { FirstName = "Salama", LastName = "Daali", Email = "salama.daali@gmail.com" }
            };

            _mockService.Setup(service => service.GetAllCandidatesAsync())
                        .ReturnsAsync(candidates);

            var result = await _controller.GetAllCandidates();

            var okResult = Assert.IsType<ActionResult<List<Candidate>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var candidatesList = Assert.IsType<List<Candidate>>(returnValue.Value);
            Assert.Equal(2, candidatesList.Count);
        }
    }
}
