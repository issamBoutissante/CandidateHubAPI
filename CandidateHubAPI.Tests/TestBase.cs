using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateHubAPI.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected readonly CandidateDbContext DbContext;
        protected readonly IMapper Mapper;

        protected TestBase()
        {
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new CandidateDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Mapper = config.CreateMapper();
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
