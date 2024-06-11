using CandidateHubAPI.Models;

namespace CandidateHubAPI.Services
{
    public interface ICandidateService
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task AddOrUpdateCandidateAsync(Candidate candidate);
        Task<List<Candidate>> GetAllCandidatesAsync();
    }
}
