using Application.DTOs.TransactionDTO;
using Dapper;
using Domain.Contracts;
using Domain.Database;
using Infrastructure.GenericRepository;
using Infrastructure.IRepository;

namespace Infrastructure.Repository
{
    public class TransactionRepo : GenericRepository<Transaction, string>, ITransactionRepo
    {


        public TransactionRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<TransactionResponseDTO>> GetAllForAdmin()
        {
            var sql = @"
                SELECT 
                    T.Id,
                    SUser.name AS SenderName,
                    RWallet.AccountNumber AS ReceiverAccountNumber,
                    T.TransactionType,
                    T.Amount,
                    T.LastModifiedBy,
                    T.LastModifiedOn
                FROM TransactionTable T
                JOIN Wallet SWallet ON T.SenderWalletId = SWallet.Id
                JOIN Users SUser ON SWallet.UserId = SUser.Id
                JOIN Wallet RWallet ON T.ReceiverWalletId = RWallet.Id
                WHERE T.IsDeleted = 0
                ORDER BY T.LastModifiedOn DESC;
            ";

            var result = await _connection.QueryAsync<TransactionResponseDTO>(sql);
            return result.ToList();
        }
        public async Task<List<TransactionHistoryDto>> GetAllTransactionsHistory()
        {
            var sql = @"
                SELECT 
                    T.Id,
                    T.CreatedOn,
                    S.Name AS SenderName,
                    R.Name AS ReceiverName,
                    T.Amount,
                    T.TransactionType,
                    T.Name AS Description
                FROM TransactionTable T
                LEFT JOIN Wallet S ON T.SenderWalletId = S.Id
                LEFT JOIN Wallet R ON T.ReceiverWalletId = R.Id
                WHERE T.IsDeleted = 0
                ORDER BY T.CreatedOn DESC;
            ";

            try
            {
                var result = await _connection.QueryAsync<TransactionHistoryDto>(sql);
                return result.ToList();
            }
            catch (Exception ex)
            {


                return new List<TransactionHistoryDto>(); // Or throw, depending on your error handling policy
            }
        }



    }
}
