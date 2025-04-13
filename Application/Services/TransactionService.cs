using Application.DTOs.TransactionDTO;
using Application.Interfaces;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<Transaction, string> _transactionRepository;
        private readonly IGenericRepository<Wallet, string> _walletRepository;

        public TransactionService(IUnit unit)
        {
            _unit = unit;
            _transactionRepository = _unit.GetRepository<Transaction, string>();
            _walletRepository = _unit.GetRepository<Wallet, string>();
        }

        public async Task<string> CreateTransaction(TransactionRequestDTO dto)
        {
            // Get sender wallet
            var senderWallet = await _walletRepository.Get(dto.SenderWalletId);
            if (senderWallet == null)
                throw new Exception("Sender wallet not found");

            // Get receiver wallet by AccountNumber
            var allWallets = await _walletRepository.GetAll("");
            var receiverWallet = allWallets.FirstOrDefault(w => w.AccountNumber == dto.ReceiverAccountNumber);
            if (receiverWallet == null)
                throw new Exception("Receiver wallet not found");

            // Check sufficient balance
            if (senderWallet.Balance < dto.Amount)
                throw new Exception("Insufficient balance");

            // Deduct from sender
            var previousSenderBalance = senderWallet.Balance;
            senderWallet.Balance -= dto.Amount;

            // Add to receiver
            var previousReceiverBalance = receiverWallet.Balance;
            receiverWallet.Balance += dto.Amount;

            // Save wallets
            await _walletRepository.Update(senderWallet);
            await _walletRepository.Update(receiverWallet);

            // Create transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                SenderWalletId = dto.SenderWalletId,
                ReceiverWalletId = receiverWallet.Id,
                TransactionType = dto.TransactionType.ToString(),
                Amount = dto.Amount,
                CreatedBy = senderWallet.Name,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = senderWallet.Name,
                LastModifiedOn = DateTime.UtcNow,
                IsDeleted = false
            };
            await _transactionRepository.Add(transaction);

            return transaction.Id;
        }


        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _transactionRepository.GetAll("");
        }

        public async Task<int> CountAll()
        {
            return await _transactionRepository.Count();
        }
    }
}
