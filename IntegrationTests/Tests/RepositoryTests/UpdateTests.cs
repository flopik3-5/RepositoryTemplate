using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Entities;
using RepositoryTemplate.Data;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.RepositoryTests
{
    public class UpdateTests : Test
    {
        public UpdateTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public Task ShouldUpdateEntityInDatabase() => Run(() =>
        {
            // Arrange
            var entity = GetTestEntity();
            Context.TestEntities.Add(entity);
            Context.SaveChanges();

            var repository = new Repository<TestEntity>(Context);

            // Act
            entity.StringProperty = "updated";
            repository.Update(entity);
            Context.SaveChanges();

            // Assert
            Context.TestEntities.FirstOrDefault().StringProperty.Should().Be("updated");
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