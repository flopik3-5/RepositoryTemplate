using System;
using System.Linq;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Interfaces
{
    public interface IGroupingSpecification<TEntity, TResult> : ISpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public Expression<Func<TEntity, object>> GroupByExpression { get; set; }

        public Expression<Func<IGrouping<object, TEntity>, TResult>> TransformExpression { get; set; }
    }
}