using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Builders
{
    internal class DataSpecificationBuilder<TEntity>
        : SpecificationBuilder<DataSpecificationBuilder<TEntity>, DataSpecification<TEntity>, TEntity, TEntity>
        where TEntity : IEntity
    {
        protected override DataSpecificationBuilder<TEntity> Builder => this;
    }
}