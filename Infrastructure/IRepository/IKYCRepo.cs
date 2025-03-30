using Domain.Contracts;
using Infrastructure.GenericRepo;


namespace Infrastructure.IRepository;
public interface IKYCRepo: IGenericRepository<KYC, string>
{
    //CustomRepositoryMethods
}