namespace CandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatesController"/> class.
        /// </summary>
        /// <param name="candidateService">The candidate service.</param>
        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
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
                return BadRequest(ModelState);
            // Add or update the candidate
            var createdCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            // Return the result
            return Ok(createdCandidateDto);
        }

        /// <summary>
        /// Gets all candidates.
        /// </summary>
        /// <returns>A list of all candidates as CandidateDto.</returns>
        [HttpGet(nameof(GetAllCandidates))]
        public async Task<ActionResult<List<CandidateDto>>> GetAllCandidates()
        {
            // Get all candidates from the service
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }
    }
}
