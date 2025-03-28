using Domain.Database;
using Domain.Entities;
using Infrastructure.GenericRepository;

namespace Infrastructure.UnitOfWork
{
    public class Unit : IUnit
    {
        private readonly ApplicationDbContext _context;

        public Unit(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<T, TId> GetRepository<T, TId>() where T : class, IEntity<TId>
        {
            return new GenericRepository<T, TId>(_context);
        }

    }
}
