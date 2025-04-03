using Domain.Contracts;
using Infrastructure.GenericRepo;


namespace Infrastructure.IRepository;
public interface IKYCRepo: IGenericRepository<KYC, string>
{
    Task<int> UpdateStatus(KYC kyc);
    
}