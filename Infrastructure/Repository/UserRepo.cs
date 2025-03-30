using Dapper;
using Domain.Contracts;
using Domain.Database;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;
namespace Infrastructure.Repository;

public class UserRepo : GenericRepository<User, string>, IUserRepo
{
    public UserRepo(ApplicationDbContext context) : base(context)
    {

    }
    public async Task<User> GetUserByRefreshToken(string refreshToken)
    {
        string query = "SELECT * FROM Users WHERE RefreshToken = @RefreshToken";
        return await _connection.QueryFirstOrDefaultAsync<User>(query, new { RefreshToken = refreshToken });
    }

    public async Task<User> FindByEmailAsync(string email)
    {

        string query = $"SELECT * FROM {TABLE_NAME} WHERE LOWER(Email) = @Email LIMIT 1";

        return await _connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email.ToLower() });
    }

    public async Task<bool> UpdateRefreshToken(string userId, string refreshToken, DateTime expiryDate)
    {
        string query = $"UPDATE {TABLE_NAME} SET RefreshToken = @RefreshToken, RefreshTokenExpiry = @ExpiryDate WHERE id = @UserId";

        int rowAffected = await _connection.ExecuteAsync(query, new { RefreshToken = refreshToken, ExpiryDate = expiryDate, UserId = userId });

        return rowAffected > 0;
    }

}

