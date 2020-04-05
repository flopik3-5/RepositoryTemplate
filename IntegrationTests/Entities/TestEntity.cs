using System;
using RepositoryTemplate.Data;

namespace IntegrationTests.Entities
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; set; }

        public string StringProperty { get; set; }

        public int IntegerProperty { get; set; }

        public float FloatProperty { get; set; }

        public double DoubleProperty { get; set; }

        public decimal DecimalProperty { get; set; }
    }
}