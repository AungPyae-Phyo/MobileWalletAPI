using Dapper;
using Domain.Contracts;
using Domain.Database;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;


namespace Infrastructure.Repository;

    public class UserRepo:GenericRepository<User, string>, IUserRepo
    {
        public UserRepo (ApplicationDbContext context) :base (context)
        {

        }
    public async Task<User> GetUserByRefreshToken(string refreshToken)
    {
        string query = "SELECT * FROM Users WHERE refresh_token = @RefreshToken";
        return await _connection.QueryFirstOrDefaultAsync<User>(query, new { RefreshToken = refreshToken});
    }

    public async Task<User> FindByEmailAsync(string email)
    {

        string query = $"SELECT * FROM {TABLE_NAME} WHERE LOWER(Email) = @Email LIMIT 1";

        return await _connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email.ToLower() });
    }



}

