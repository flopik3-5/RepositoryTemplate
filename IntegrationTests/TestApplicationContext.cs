using IntegrationTests.Entities;
using IntegrationTests.Loggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryTemplate;
using Xunit.Abstractions;

namespace IntegrationTests
{
    public class TestApplicationContext : ApplicationContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        private readonly ITestOutputHelper _outputHelper;

        public TestApplicationContext(ITestOutputHelper outputHelper) 
            : base(new DbContextOptionsBuilder()
                .UseNpgsql(Constants.ConnectionString)
                .Options)
        {
            _outputHelper = outputHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new SqlLoggerProvider(_outputHelper));
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}