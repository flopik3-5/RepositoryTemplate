using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace IntegrationTests.Tests
{
    public class Test : IDisposable
    {
        protected readonly TestApplicationContext Context;

        protected Test(ITestOutputHelper outputHelper)
        {
            Context = new TestApplicationContext(outputHelper);
            Context.Database.Migrate();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        protected async Task Run(Func<Task> action)
        {
            await Context.Database.BeginTransactionAsync();

            try
            {
                await action();
            }
            finally
            {
                Context.Database.RollbackTransaction();
            }
        }

        protected async Task Run(Action action)
        {
            await Context.Database.BeginTransactionAsync();

            try
            {
                action();
            }
            finally
            {
                Context.Database.RollbackTransaction();
            }
        }
    }
}