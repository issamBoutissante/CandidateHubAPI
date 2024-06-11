﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CandidateHubAPI.Tests
{
    public class CandidateServiceTests : TestBase
    {
        private readonly CandidateService _candidateService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CandidateService> _logger;

        public CandidateServiceTests()
            : base()
        {
            // Initialize IMemoryCache
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            // Initialize ILogger<CandidateService>
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<CandidateService>();

            // Initialize CandidateService with the necessary dependencies
            _candidateService = new CandidateService(DbContext, Mapper, _memoryCache, _logger);
        }

        /// <summary>
        /// Tests that a new candidate is added successfully.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldAddCandidate_WhenNew()
        {
            ClearDatabase();

            var candidateDto = new CandidateDto
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            var createdCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            var savedCandidate = await _candidateService.GetCandidateByEmailAsync(candidateDto.Email);

            Assert.NotNull(savedCandidate);
            Assert.Equal("Issam", savedCandidate.FirstName);
            Assert.Equal("Boutissante", savedCandidate.LastName);
        }

        /// <summary>
        /// Tests that an existing candidate is updated successfully.
        /// </summary>
        [Fact]
        public async Task AddOrUpdateCandidate_ShouldUpdateCandidate_WhenExisting()
        {
            ClearDatabase();

            var candidateDto = new CandidateDto
            {
                FirstName = "Issam",
                LastName = "Boutissante",
                PhoneNumber = "1234567890",
                Email = "boutissante.issam.dev@gmail.com",
                LinkedInProfileUrl = "https://www.linkedin.com/in/issam-boutissante-1194a1205/",
                GitHubProfileUrl = "https://github.com/issamBoutissante",
                Comment = "Hi I'm a full stack .net developer and currently I'm creating the software for applying in Sigma Company!"
            };

            var createdCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            candidateDto.LastName = "Daali";

            var updatedCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            var savedCandidate = await _candidateService.GetCandidateByEmailAsync(candidateDto.Email);

            Assert.NotNull(savedCandidate);
            Assert.Equal("Daali", savedCandidate.LastName);
        }

        /// <summary>
        /// Tests that all candidates are returned successfully.
        /// </summary>
        [Fact]
        public async Task GetAllCandidates_ShouldReturnAllCandidates()
        {
            ClearDatabase();

            var candidates = new List<CandidateDto>
            {
                new CandidateDto { FirstName = "Issam", LastName = "Boutissante", Email = "boutissante.issam.dev@gmail.com", PhoneNumber = "1234567890" },
                new CandidateDto { FirstName = "Salama", LastName = "Daali", Email = "salama.daali@gmail.com", PhoneNumber = "0987654321" }
            };

            await _candidateService.AddOrUpdateCandidateAsync(candidates[0]);
            await _candidateService.AddOrUpdateCandidateAsync(candidates[1]);

            var result = await _candidateService.GetAllCandidatesAsync();

            Assert.Equal(2, result.Count);
        }
    }
}
