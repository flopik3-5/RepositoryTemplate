using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class AddTests : Test
    {
        public AddTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldAddEntityToDatabase() => Run(async () =>
        {
            // Arrange
            var repository = new Repository<TestEntity>(Context);
            var entity = GetTestEntity();

            // Act
            await repository.Add(entity);
            Context.SaveChanges();

            // Assert
            Context.TestEntities.Should().BeEquivalentTo(entity);
        });

        [Fact]
        public Task ShouldSetEntityId() => Run(async () =>
        {
            // Arrange
            var repository = new Repository<TestEntity>(Context);
            var entity = GetTestEntity();

            // Act
            await repository.Add(entity);

            // Assert
            entity.Id.Should().NotBeEmpty();
        });

        private TestEntity GetTestEntity()
        {
            return new TestEntity
            {
                StringProperty = "data",
                DecimalProperty = 42,
            };
        }
    }
}