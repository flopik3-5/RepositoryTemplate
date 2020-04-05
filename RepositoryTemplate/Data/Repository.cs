using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Data
{
    internal class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationContext _context;

        private DbSet<TEntity> Set => _context.Set<TEntity>();

        private IQueryable<TEntity> Query => Set.AsQueryable();

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Get(Guid id) => await Set.FindAsync(id);

        public Task<TResult> SingleOrDefault<TResult>(ISpecification<TEntity, TResult> specification) =>
            Query.ApplySpecification(specification).SingleOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> ListAll() => await Set.ToListAsync();

        public async Task<IReadOnlyList<TResult>> List<TResult>(ISpecification<TEntity, TResult> specification) =>
            await Query.ApplySpecification(specification).ToListAsync();

        public Task Add(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
            }

            return Set.AddRangeAsync(entities);
        }

        public void Update(params TEntity[] entities) => Set.UpdateRange(entities);

        public async Task Delete(Guid id) => Delete(await Get(id));

        public void Delete(params TEntity[] entities) => Set.RemoveRange(entities);

        public async Task Delete(IDataSpecification<TEntity> specification) =>
            Set.RemoveRange(await List(specification));

        public Task<int> Count(IDataSpecification<TEntity> specification) =>
            Query.ApplySpecification(specification).CountAsync();

        public Task<decimal> Sum(ITransformationSpecification<TEntity, decimal> specification) =>
            Query.ApplySpecification(specification).SumAsync();

        public Task<decimal?> Average(ITransformationSpecification<TEntity, decimal?> specification) =>
            Query.ApplySpecification(specification).AverageAsync();
    }
}