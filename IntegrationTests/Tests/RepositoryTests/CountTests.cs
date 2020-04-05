using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Builders;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class CountTests : Test
    {
        public CountTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldReturnNumberOfEntitiesSatisfyingSpecification() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithFilter(t => t.DecimalProperty > 30)
                .Build();

            // Act
            var result = await repository.Count(specification);

            // Assert
            result.Should().Be(1);
        });

        private IEnumerable<TestEntity> GetTestEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    DecimalProperty = 42,
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    DecimalProperty = 22,
                },
            };
        }
    }
}