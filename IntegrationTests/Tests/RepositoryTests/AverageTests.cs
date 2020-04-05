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
    public class AverageTests : Test
    {
        public AverageTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task FilteredSetIsEmpty_ShouldReturnNull() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, decimal?>()
                .WithFilter(t => t.IntegerProperty > 120)
                .WithTransform(t => t.IntegerProperty)
                .Build();

            // Act
            var result = await repository.Average(specification);

            // Assert
            result.Should().BeNull();
        });

        [Fact]
        public Task ShouldReturnAverageOfIntEntitiesPropertiesSatisfyingSpecification() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, decimal?>()
                .WithFilter(t => t.IntegerProperty > 20)
                .WithTransform(t => t.IntegerProperty)
                .Build();

            // Act
            var result = await repository.Average(specification);

            // Assert
            result.Should().Be(32);
        });

        [Fact]
        public Task ShouldReturnAverageOfFloatEntitiesPropertiesSatisfyingSpecification() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, decimal?>()
                .WithFilter(t => t.FloatProperty > 20)
                .WithTransform(t => (decimal) t.FloatProperty)
                .Build();

            // Act
            var result = await repository.Average(specification);

            // Assert
            result.Should().Be(32);
        });

        [Fact]
        public Task ShouldReturnAverageOfDoubleEntitiesPropertiesSatisfyingSpecification() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, decimal?>()
                .WithFilter(t => t.DoubleProperty > 20)
                .WithTransform(t => (decimal) t.DoubleProperty)
                .Build();

            // Act
            var result = await repository.Average(specification);

            // Assert
            result.Should().Be(32);
        });

        [Fact]
        public Task ShouldReturnAverageOfDecimalEntitiesPropertiesSatisfyingSpecification() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, decimal?>()
                .WithFilter(t => t.DecimalProperty > 20)
                .WithTransform(t => t.DecimalProperty)
                .Build();

            // Act
            var result = await repository.Average(specification);

            // Assert
            result.Should().Be(32);
        });

        private IEnumerable<TestEntity> GetTestEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    IntegerProperty = 42,
                    FloatProperty = 42.0f,
                    DoubleProperty = 42.0,
                    DecimalProperty = 42.0m,
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    IntegerProperty = 22,
                    FloatProperty = 22.0f,
                    DoubleProperty = 22.0,
                    DecimalProperty = 22.0m,
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    IntegerProperty = 15,
                    FloatProperty = 15.0f,
                    DoubleProperty = 15.0,
                    DecimalProperty = 15.0m,
                },
            };
        }
    }
}