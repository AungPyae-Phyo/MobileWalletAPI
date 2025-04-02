using Application.DTOs.UserDTO;
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

    public async Task<IEnumerable<UserWithWalletAndKYCDto>> GetAllUsersWithWalletAndKYC()
    {
        string query = @"
            SELECT 
                u.CreatedOn,
                u.Name AS UserName,
                u.PhoneNumber,
                u.Email,
                u.Role,
                w.Id AS WalletID,
                CASE 
                    WHEN k.UserID IS NOT NULL THEN k.Status  
                    ELSE 'Pending' 
                END AS Status,
                w.Balance
            FROM Users u
            JOIN Wallet w ON u.Id = w.UserId
            LEFT JOIN KYC k ON u.Id = k.UserID
            ORDER BY u.CreatedOn DESC"; 
        return await _connection.QueryAsync<UserWithWalletAndKYCDto>(query);
    }

}



