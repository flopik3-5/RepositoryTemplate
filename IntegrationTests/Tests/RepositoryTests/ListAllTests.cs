using System;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class ListAllTests : Test
    {
        public ListAllTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldReturnCorrectEntities() => Run(async () =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);

            // Act
            var result = await repository.ListAll();

            // Assert
            result.Should().BeEquivalentTo(entity);
        });

        private TestEntity GetTestEntity()
        {
            return new TestEntity
            {
                Id = Guid.NewGuid(),
                StringProperty = "data",
                DecimalProperty = 42,
            };
        }
    }
}