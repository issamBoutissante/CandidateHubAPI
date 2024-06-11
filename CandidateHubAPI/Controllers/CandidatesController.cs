namespace CandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly ILogger<CandidatesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatesController"/> class.
        /// </summary>
        /// <param name="candidateService">The candidate service.</param>
        /// <param name="logger">The logger instance.</param>
        public CandidatesController(ICandidateService candidateService, ILogger<CandidatesController> logger)
        {
            _candidateService = candidateService;
            _logger = logger;
        }

        /// <summary>
        /// Adds or updates a candidate.
        /// </summary>
        /// <param name="candidateDto">The candidate DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost(nameof(AddOrUpdateCandidate))]
        public async Task<ActionResult<Candidate>> AddOrUpdateCandidate(CandidateDto candidateDto)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for candidate with email: {Email}", candidateDto.Email);
                return BadRequest(ModelState);
            }

            try
            {
                // Add or update the candidate
                var createdCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

                // Return the result
                return Ok(createdCandidateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding or updating candidate with email: {Email}", candidateDto.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all candidates.
        /// </summary>
        /// <returns>A list of all candidates as CandidateDto.</returns>
        [HttpGet(nameof(GetAllCandidates))]
        public async Task<ActionResult<List<CandidateDto>>> GetAllCandidates()
        {
            try
            {
                // Get all candidates from the service
                var candidates = await _candidateService.GetAllCandidatesAsync();
                return Ok(candidates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all candidates");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
