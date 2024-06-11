namespace CandidateHubAPI.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected readonly CandidateDbContext DbContext;
        protected readonly IMapper Mapper;
        protected readonly IMemoryCache MemoryCache;
        protected readonly ILoggerFactory LoggerFactory;

        protected TestBase()
        {
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new CandidateDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Mapper = config.CreateMapper();

            MemoryCache = new MemoryCache(new MemoryCacheOptions());

            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
        }

        protected void ClearDatabase()
        {
            DbContext.Candidates.RemoveRange(DbContext.Candidates);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
