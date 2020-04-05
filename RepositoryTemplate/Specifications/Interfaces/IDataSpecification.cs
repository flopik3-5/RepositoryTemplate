using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Interfaces
{
    public interface IDataSpecification<TEntity> : ISpecification<TEntity, TEntity>
        where TEntity : IEntity
    {
    }
}