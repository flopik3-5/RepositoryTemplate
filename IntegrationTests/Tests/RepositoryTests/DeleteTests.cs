using System;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Builders;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class DeleteTests : Test
    {
        public DeleteTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ById_ShouldDeleteEntityFromDatabase() => Run(async () =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);

            // Act
            await repository.Delete(entity.Id);
            Context.SaveChanges();

            // Assert
            Context.TestEntities.Should().BeEmpty();
        });

        [Fact]
        public Task ByEntity_ShouldDeleteEntityFromDatabase() => Run(() =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);

            // Act
            repository.Delete(entity);
            Context.SaveChanges();

            // Assert
            Context.TestEntities.Should().BeEmpty();
        });

        [Fact]
        public Task BySpecification_ShouldDeleteEntityFromDatabase() => Run(async () =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithFilter(t=>t.DecimalProperty > 2)
                .Build();

            // Act
            await repository.Delete(specification);
            Context.SaveChanges();

            // Assert
            Context.TestEntities.Should().BeEmpty();
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