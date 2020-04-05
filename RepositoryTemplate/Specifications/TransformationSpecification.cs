using System;
using System.Linq.Expressions;
using RepositoryTemplate.Data;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Specifications
{
    internal class TransformationSpecification<TEntity, TResult> : Specification<TEntity, TResult>,
        ITransformationSpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        public Expression<Func<TEntity, TResult>> TransformExpression { get; set; }
    }
}