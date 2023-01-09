using Ardalis.Specification;
using Domain.Common;

namespace Application.Common.Interfaces;

public interface IRepository<TEntity> : IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
}