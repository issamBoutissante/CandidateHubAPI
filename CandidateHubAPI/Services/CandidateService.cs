using CandidateHubAPI.Data;
using CandidateHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDbContext _context;

        public CandidateService(CandidateDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await GetCandidateByEmailAsync(candidate.Email);
            if (existingCandidate == null)
            {
                await _context.Candidates.AddAsync(candidate);
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeIntervalStart = candidate.CallTimeIntervalStart;
                existingCandidate.CallTimeIntervalEnd = candidate.CallTimeIntervalEnd;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }
    }
}
