using System;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class GetTests : Test
    {
        public GetTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldReturnCorrectEntity() => Run(async () =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);

            // Act
            var result = await repository.Get(entity.Id);

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