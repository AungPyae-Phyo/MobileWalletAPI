using Domain.Contracts;
using Domain.Database;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;


namespace Infrastructure.Repository
{
    public class KYCRepo:GenericRepository<KYC, string>, IKYCRepo
    {
        public KYCRepo(ApplicationDbContext context) : base(context)
        {
        }

        //Custom Methods

    }
}
