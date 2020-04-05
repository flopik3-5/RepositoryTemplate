using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace IntegrationTests.Loggers
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _outputHelper;

        public SqlLoggerProvider(ITestOutputHelper outputHelper) => _outputHelper = outputHelper;

        public ILogger CreateLogger(string categoryName) => new SqlLogger(_outputHelper);

        public void Dispose()
        {
        }
    }
}