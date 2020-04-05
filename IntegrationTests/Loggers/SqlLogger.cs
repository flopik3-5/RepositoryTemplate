using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace IntegrationTests.Loggers
{
    public class SqlLogger : ILogger
    {
        private readonly ITestOutputHelper _outputHelper;

        private readonly string[] _ignoredTables =
        {
            "pg_catalog",
            "__EFMigrationsHistory",
        };

        public SqlLogger(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId != 20101)
            {
                return;
            }

            var message = formatter(state, exception);
            if (_ignoredTables.Any(t => message.Contains(t)))
            {
                return;
            }

            _outputHelper.WriteLine(message);
            _outputHelper.WriteLine(string.Empty);
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}