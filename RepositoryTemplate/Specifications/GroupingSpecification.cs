using System;
using System.Linq;
using System.Linq.Expressions;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Specifications
{
    internal class GroupingSpecification<TEntity, TResult> : Specification<TEntity, TResult>,
        IGroupingSpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public Expression<Func<TEntity, object>> GroupByExpression { get; set; }

        public Expression<Func<IGrouping<object, TEntity>, TResult>> TransformExpression { get; set; }
    }
}