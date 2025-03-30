using Domain.Contracts;
using Infrastructure.GenericRepo;


namespace Infrastructure.IRepository;

public interface IUserRepo : IGenericRepository<User,string>
{
    Task<User> FindByEmailAsync(string email);
    Task<User> GetUserByRefreshToken(string refreshToken);

    Task<bool> UpdateRefreshToken(string userId, string refreshToken, DateTime expiryDate);

}
