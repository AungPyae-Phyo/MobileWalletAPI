using Domain.Entities;
using Infrastructure.GenericRepository;


namespace Infrastructure.UnitOfWork
{
    public interface IUnit
    {
        GenericRepository<T, TId> GetRepository<T, TId>() where T : class, IEntity<TId>;

    }
}
