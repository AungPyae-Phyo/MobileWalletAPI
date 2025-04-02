using Domain.Database;
using Domain.Entities;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    public class Unit : IUnit
    {
        private readonly ApplicationDbContext _context;
        private KYCRepo _kycRepo;

        public Unit(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<T, TId> GetRepository<T, TId>() where T : class, IEntity<TId>
        {
            return new GenericRepository<T, TId>(_context);
        }
        public IKYCRepo KYCRepo => _kycRepo ??= new KYCRepo(_context);

    }
}
