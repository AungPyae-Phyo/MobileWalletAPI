using Application.DTOs.UserDTO;
using Dapper;
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
        public async Task<int> UpdateStatus(KYC kyc)
        {
            string query = "UPDATE KYC SET Status = @Status WHERE Id = @Id";
            return await _connection.ExecuteAsync(query, new { Status = kyc.Status.ToString(), kyc.Id });

        }
    }
}
