namespace CandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CandidateDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CandidateService> _logger;

        private const string CandidatesCacheKey = "allCandidates";
        private const string CandidateCacheKeyPrefix = "candidate_";

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="cache">The memory cache.</param>
        /// <param name="logger">The logger instance.</param>
        public CandidateService(CandidateDbContext context, IMapper mapper, IMemoryCache cache, ILogger<CandidateService> logger)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Gets a candidate by email.
        /// </summary>
        /// <param name="email">The candidate's email.</param>
        /// <returns>The candidate with the specified email.</returns>
        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            var cacheKey = $"{CandidateCacheKeyPrefix}{email}";
            if (!_cache.TryGetValue(cacheKey, out Candidate candidate))
            {
                _logger.LogInformation("Cache miss for candidate with email: {Email}", email);
                candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
                if (candidate != null)
                {
                    _cache.Set(cacheKey, candidate, TimeSpan.FromMinutes(30));
                    _logger.LogInformation("Candidate with email: {Email} cached", email);
                }
            }
            return candidate;
        }

        /// <summary>
        /// Adds or updates a candidate.
        /// </summary>
        /// <param name="candidateDto">The candidate DTO.</param>
        /// <returns>The added or updated candidate DTO.</returns>
        public async Task<Candidate> AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            try
            {
                var candidate = await GetCandidateByEmailAsync(candidateDto.Email);
                if (candidate == null)
                {
                    // Add new candidate if it does not exist
                    candidate = _mapper.Map<Candidate>(candidateDto);
                    await _context.Candidates.AddAsync(candidate);
                    _logger.LogInformation("Adding new candidate with email: {Email}", candidateDto.Email);
                }
                else
                {
                    // Update existing candidate
                    _mapper.Map(candidateDto, candidate);
                    _logger.LogInformation("Updating existing candidate with email: {Email}", candidateDto.Email);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Invalidate cache
                _cache.Remove(CandidatesCacheKey);
                _cache.Remove($"{CandidateCacheKeyPrefix}{candidate.Email}");

                return candidate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding or updating candidate with email: {Email}", candidateDto.Email);
                throw;
            }
        }

        /// <summary>
        /// Gets all candidates and maps them to CandidateDto.
        /// </summary>
        /// <returns>A list of all candidates as CandidateDto.</returns>
        public async Task<List<CandidateDto>> GetAllCandidatesAsync()
        {
            if (!_cache.TryGetValue(CandidatesCacheKey, out List<Candidate> candidates))
            {
                _logger.LogInformation("Cache miss for all candidates");
                candidates = await _context.Candidates.ToListAsync();
                _cache.Set(CandidatesCacheKey, candidates, TimeSpan.FromMinutes(30));
                _logger.LogInformation("All candidates cached");
            }

            return _mapper.Map<List<CandidateDto>>(candidates);
        }
    }
}
