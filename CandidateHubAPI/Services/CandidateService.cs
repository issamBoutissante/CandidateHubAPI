namespace CandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDbContext _context;
        private readonly IMapper _mapper;

        public CandidateService(CandidateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                _mapper.Map(candidate, existingCandidate);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }
    }
}
