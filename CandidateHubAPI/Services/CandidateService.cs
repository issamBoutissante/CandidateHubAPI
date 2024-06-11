namespace CandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CandidateService(CandidateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a candidate by email.
        /// </summary>
        /// <param name="email">The candidate's email.</param>
        /// <returns>The candidate with the specified email.</returns>
        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        /// <summary>
        /// Adds or updates a candidate.
        /// </summary>
        /// <param name="candidateDto">The candidate DTO.</param>
        /// <returns>The added or updated candidate DTO.</returns>
        public async Task<Candidate> AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            var candidate = await GetCandidateByEmailAsync(candidateDto.Email);
            if (candidate == null)
            {
                // Add new candidate if it does not exist
                candidate = _mapper.Map<Candidate>(candidateDto);
                await _context.Candidates.AddAsync(candidate);
            }
            else
            {
                // Update existing candidate
                _mapper.Map(candidateDto, candidate);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            return candidate;
        }

        /// <summary>
        /// Gets all candidates and maps them to CandidateDto.
        /// </summary>
        /// <returns>A list of all candidates as CandidateDto.</returns>
        public async Task<List<CandidateDto>> GetAllCandidatesAsync()
        {
            var candidates = await _context.Candidates.ToListAsync();
            return _mapper.Map<List<CandidateDto>>(candidates);
        }
    }
}
