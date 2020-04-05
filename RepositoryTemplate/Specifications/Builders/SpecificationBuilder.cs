using System;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Builders
{
    internal abstract class SpecificationBuilder<TBuilder, TSpecification, TEntity, TResult>
        where TEntity : IEntity
        where TSpecification : Specification<TEntity, TResult>, new()
    {
        protected abstract TBuilder Builder { get; }

        protected readonly TSpecification Specification = new TSpecification();

        public TSpecification Build() => Specification;

        public TBuilder DataAsReadOnly()
        {
            Specification.IsReadOnly = true;

            return Builder;
        }

        public TBuilder WithInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            Specification.IncludeExpressions.AddRange(includes);

            return Builder;
        }

        public TBuilder WithInclude(params string[] includes)
        {
            Specification.IncludeStrings.AddRange(includes);

            return Builder;
        }

        public TBuilder WithFilter(Expression<Func<TEntity, bool>> filterExpression)
        {
            Specification.FilterExpression = filterExpression;

            return Builder;
        }

        public TBuilder WithOrderByAscending(Expression<Func<TEntity, object>> orderByExpression)
        {
            Specification.OrderingType = OrderingType.Ascending;
            Specification.OrderByExpression = orderByExpression;

            return Builder;
        }

        public TBuilder WithOrderByDescending(Expression<Func<TEntity, object>> orderByExpression)
        {
            Specification.OrderingType = OrderingType.Descending;
            Specification.OrderByExpression = orderByExpression;

            return Builder;
        }

        public TBuilder SetPage(int pageNumber, int pageSize)
        {
            Specification.IsPagingEnabled = true;
            Specification.Skip = pageNumber * pageSize;
            Specification.Take = pageSize;

            return Builder;
        }
    }
}