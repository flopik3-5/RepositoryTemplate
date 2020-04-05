using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Interfaces
{
    public interface ISpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public bool IsReadOnly { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPagingEnabled { get; set; }

        public List<string> IncludeStrings { get; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }

        public OrderingType OrderingType { get; set; }

        public Expression<Func<TEntity, object>> OrderByExpression { get; set; }
    }
}