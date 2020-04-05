using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Specifications
{
    internal abstract class Specification<TEntity, TResult> : ISpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public bool IsReadOnly { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPagingEnabled { get; set; }

        public List<string> IncludeStrings { get; } = new List<string>();

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } =
            new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }

        public OrderingType OrderingType { get; set; } = OrderingType.None;

        public Expression<Func<TEntity, object>> OrderByExpression { get; set; }
    }
}