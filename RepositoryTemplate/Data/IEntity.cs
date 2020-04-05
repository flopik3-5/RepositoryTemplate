using System;

namespace RepositoryTemplate.Data
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}