using Domain.Entities;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;


namespace Infrastructure.UnitOfWork
{
    public interface IUnit
    {
        GenericRepository<T, TId> GetRepository<T, TId>() where T : class, IEntity<TId>;
        IKYCRepo KYCRepo { get; }
    }
}
