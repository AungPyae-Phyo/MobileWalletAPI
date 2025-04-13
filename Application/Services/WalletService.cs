using Application.DTOs.WalletDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<User, string> _userRepository;
        private readonly IGenericRepository<Wallet, string> _walletRepository;
        private readonly IGenericRepository<WalletHistory, string> _walletHistoryRepo;
        private readonly IMapper _mapper;

        public WalletService(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _walletRepository = _unit.GetRepository<Wallet, string>();
            _userRepository = _unit.GetRepository<User, string>();
            _walletHistoryRepo = _unit.GetRepository<WalletHistory, string>(); // ✅ FIXED
            _mapper = mapper;
        }

        public async Task<int> Create(Wallet walletDto)
        {
            var userExists = await _userRepository.Get(walletDto.UserId);
            if (userExists == null)
                throw new Exception("User not found. Only registered users can have a Wallet.");

            var existingWallet = await _walletRepository.Get(walletDto.UserId);
            if (existingWallet != null)
                throw new Exception("User already has a Wallet.");

            var wallet = _mapper.Map<Wallet>(walletDto);
            wallet.Id = Guid.NewGuid().ToString();
            wallet.Balance = 0;
            wallet.UserId = walletDto.UserId;

            return await _walletRepository.Add(wallet);
        }

        public async Task<WalletDTO> Get(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
                throw new Exception("Wallet not found.");

            return _mapper.Map<WalletDTO>(wallet);
        }

        public async Task<Wallet> UpdateBalance(string walletId, decimal amount)
        {
            var wallet = await _walletRepository.Get(walletId);
            if (wallet == null)
                throw new Exception($"Wallet not found for walletId: {walletId}");

            if (wallet.Balance + amount < 0)
                throw new Exception("Insufficient balance.");

            var previousBalance = wallet.Balance;

            wallet.LastModifiedBy = "Admin";
            wallet.Balance += amount;

            await _walletRepository.Update(wallet);

            var history = new WalletHistory
            {
                Id = Guid.NewGuid().ToString(),
                WalletId = wallet.Id,
                ChangeAmount = amount,
                LastModifiedBy = "Admin",
                PreviousBalance = (decimal)previousBalance,
                NewBalance = (decimal)wallet.Balance,
            };

            await _walletHistoryRepo.Add(history);

            return wallet;
        }

        public async Task<Wallet> FindByAccountNumber(string accountNumber)
        {
            var allWallets = await _walletRepository.GetAll("");
            return allWallets.FirstOrDefault(w => w.AccountNumber == accountNumber);
        }



        public async Task<int> Update(WalletDTO walletDto)
        {
            var wallet = await _walletRepository.Get(walletDto.WalletId);
            if (wallet == null)
                throw new Exception("Wallet not found.");

            wallet.Balance = walletDto.Balance;
            return await _walletRepository.Update(wallet);
        }

        public async Task<int> Delete(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
                throw new Exception("Wallet not found.");

            return await _walletRepository.Delete(wallet);
        }

        public async Task<IEnumerable<WalletHistory>> GetWalletHistory(string walletId)
        {
            var allHistories = await _walletHistoryRepo.GetAll("");
            var history = allHistories
                .Where(h => h.WalletId == walletId);
                
            return history;
        }



        public async Task<int> CountAll()
        {
            return await _walletRepository.Count();
        }

        public async Task<bool> SoftDelete(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
                throw new Exception("Wallet record not found.");

            return await _walletRepository.SoftDelete(wallet) > 0;
        }

        public async Task<IEnumerable<Wallet>> GetAll()
        {
            return await _walletRepository.GetAll("");
        }
    }
}
