using CandidateHubAPI.Models;

namespace CandidateHubAPI.Services
{
    public interface ICandidateService
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task<Candidate> AddOrUpdateCandidateAsync(CandidateDto candidate);
        Task<List<CandidateDto>> GetAllCandidatesAsync();
    }
}
