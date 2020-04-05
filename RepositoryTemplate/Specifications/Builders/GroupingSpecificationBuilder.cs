using System;
using System.Linq;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Builders
{
    internal class GroupingSpecificationBuilder<TEntity, TResult>
        : SpecificationBuilder<
            GroupingSpecificationBuilder<TEntity, TResult>,
            GroupingSpecification<TEntity, TResult>,
            TEntity,
            TResult>
        where TEntity : IEntity
    {
        public GroupingSpecificationBuilder<TEntity, TResult> WithGroupBy(
            Expression<Func<TEntity, object>> groupByExpression,
            Expression<Func<IGrouping<object, TEntity>, TResult>> transformExpression)
        {
            Specification.GroupByExpression = groupByExpression;
            Specification.TransformExpression = transformExpression;

            return Builder;
        }

        protected override GroupingSpecificationBuilder<TEntity, TResult> Builder => this;
    }
}