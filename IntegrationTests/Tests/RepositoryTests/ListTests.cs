using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Builders;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class ListTests : Test
    {
        public ListTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldReturnCorrectEntities() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithFilter(t => t.IntegerProperty > 25)
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(new[]
            {
                new TestEntity
                {
                    StringProperty = "data1",
                    IntegerProperty = 42,
                },
            }, opt => opt.Excluding(t => t.Id));
        });

        [Fact]
        public Task ShouldReturnCorrectEntitiesOrderedByAscending() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithOrderByAscending(t => t.IntegerProperty)
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(entities)
                .And.BeInAscendingOrder(t => t.IntegerProperty);
        });

        [Fact]
        public Task ShouldReturnCorrectEntitiesOrderedByDescending() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithOrderByDescending(t => t.IntegerProperty)
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(entities)
                .And.BeInDescendingOrder(t => t.IntegerProperty);
        });

        [Fact]
        public Task ShouldReturnPagedResult() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new DataSpecificationBuilder<TestEntity>()
                .WithOrderByAscending(t => t.IntegerProperty)
                .SetPage(1, 1)
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(new[]
            {
                new TestEntity
                {
                    StringProperty = "data1",
                    IntegerProperty = 22,
                },
            }, opt => opt.Excluding(t => t.Id));
        });

        [Fact]
        public Task ShouldReturnCorrectTransformedEntities() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new TransformationSpecificationBuilder<TestEntity, TransformedEntity>()
                .WithFilter(t => t.IntegerProperty > 25)
                .WithTransform(t => new TransformedEntity { Value = t.IntegerProperty })
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(new TransformedEntity { Value = 42 });
        });

        [Fact]
        public Task ShouldReturnCorrectGroupedEntities() => Run(async () =>
        {
            // Arrange
            var entities = GetTestEntities();
            await Context.TestEntities.AddRangeAsync(entities);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);
            var specification = new GroupingSpecificationBuilder<TestEntity, GroupedEntity>()
                .WithFilter(t => t.IntegerProperty > 21)
                .WithGroupBy(t => t.StringProperty, g => new GroupedEntity
                {
                    Key = (string) g.Key,
                    Value = g.Sum(e => e.IntegerProperty),
                })
                .Build();

            // Act
            var result = await repository.List(specification);

            // Assert
            result.Should().BeEquivalentTo(new GroupedEntity
            {
                Key = "data1",
                Value = 64,
            });
        });

        private List<TestEntity> GetTestEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data1",
                    IntegerProperty = 42,
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data1",
                    IntegerProperty = 22,
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    StringProperty = "data",
                    IntegerProperty = 15,
                },
            };
        }
    }
}