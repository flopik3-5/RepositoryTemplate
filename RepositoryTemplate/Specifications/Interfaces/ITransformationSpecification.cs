using System;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Interfaces
{
    public interface ITransformationSpecification<TEntity, TResult> : ISpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public Expression<Func<TEntity, TResult>> TransformExpression { get; set; }
    }
}