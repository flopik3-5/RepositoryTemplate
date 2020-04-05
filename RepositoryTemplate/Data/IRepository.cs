using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Data
{
    public interface IRepository<TEntity> 
        where TEntity : IEntity
    {
        Task<TEntity> Get(Guid id);

        Task<TResult> SingleOrDefault<TResult>(ISpecification<TEntity, TResult> specification);

        Task<IReadOnlyList<TEntity>> ListAll();

        Task<IReadOnlyList<TResult>> List<TResult>(ISpecification<TEntity, TResult> specification);

        Task Add(params TEntity[] entities);

        void Update(params TEntity[] entities);

        Task Delete(Guid id);

        void Delete(params TEntity[] entities);

        Task Delete(IDataSpecification<TEntity> specification);

        Task<int> Count(IDataSpecification<TEntity> specification);

        Task<decimal> Sum(ITransformationSpecification<TEntity, decimal> specification);

        Task<decimal?> Average(ITransformationSpecification<TEntity, decimal?> specification);
    }
}