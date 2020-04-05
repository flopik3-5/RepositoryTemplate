using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Specifications
{
    internal class DataSpecification<TEntity> : Specification<TEntity, TEntity>, IDataSpecification<TEntity>
        where TEntity : IEntity
    {
    }
}