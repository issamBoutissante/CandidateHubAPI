using CandidateHubAPI.Dtos;
using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdateCandidate(CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = new Candidate
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                PhoneNumber = candidateDto.PhoneNumber,
                Email = candidateDto.Email,
                CallTimeIntervalStart = candidateDto.CallTimeIntervalStart,
                CallTimeIntervalEnd = candidateDto.CallTimeIntervalEnd,
                LinkedInProfileUrl = candidateDto.LinkedInProfileUrl,
                GitHubProfileUrl = candidateDto.GitHubProfileUrl,
                Comment = candidateDto.Comment
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            return Ok(candidate);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Candidate>>> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }
    }
}
