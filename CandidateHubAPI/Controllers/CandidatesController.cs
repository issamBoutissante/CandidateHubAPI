namespace CandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;

        public CandidatesController(ICandidateService candidateService, IMapper mapper)
        {
            _candidateService = candidateService;
            _mapper = mapper;
        }

        [HttpPost(nameof(AddOrUpdateCandidate))]
        public async Task<IActionResult> AddOrUpdateCandidate(CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = _mapper.Map<Candidate>(candidateDto);

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            return Ok(candidate);
        }

        [HttpGet(nameof(GetAllCandidates))]
        public async Task<ActionResult<List<Candidate>>> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }
    }
}