using Application.DTOs.WalletDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<User, string> _userRepository;
        private readonly IGenericRepository<Wallet, string> _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _walletRepository = _unit.GetRepository<Wallet, string>();
            _userRepository = _unit.GetRepository<User, string>();
            _mapper = mapper;
        }

        public async Task<int> Create(WalletDTO walletDto)
        {
            // Check if User Exists
            var userExists = await _userRepository.Get(walletDto.UserId);
            if (userExists == null)
            {
                throw new Exception("User not found. Only registered users can have a Wallet.");
            }

            // Check if Wallet Already Exists
            var existingWallet = await _walletRepository.Get(walletDto.UserId);
            if (existingWallet != null)
            {
                throw new Exception("User already has a Wallet.");
            }

            Console.WriteLine($"User with ID {walletDto.UserId} is eligible for Wallet creation.");

            // Create New Wallet
            var wallet = _mapper.Map<Wallet>(walletDto);
            wallet.Id = Guid.NewGuid().ToString();
            wallet.Balance = 0;
            wallet.UserId = walletDto.UserId;

            var result = await _walletRepository.Add(wallet);
            Console.WriteLine(result > 0 ? "Wallet created successfully." : "Failed to create Wallet.");
            return result;
        }

        public async Task<WalletDTO> Get(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found.");
            }
            return _mapper.Map<WalletDTO>(wallet);
        }

        public async Task<int> UpdateBalance(string userId, decimal amount)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found.");
            }

            if (wallet.Balance + amount < 0)
            {
                throw new Exception("Insufficient balance.");
            }

            wallet.Balance += amount;
            return await _walletRepository.Update(wallet);
        }

        public async Task<int> Update(WalletDTO walletDto)
        {
            var wallet = await _walletRepository.Get(walletDto.UserId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found.");
            }

            wallet.Balance = walletDto.Balance;
            return await _walletRepository.Update(wallet);
        }

        public async Task<int> Delete(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found.");
            }
            return await _walletRepository.Delete(wallet);
        }

        public async Task<int> CountAll()
        {
            return await _walletRepository.Count();
        }

        public async Task<bool> SoftDelete(string userId)
        {
            var wallet = await _walletRepository.Get(userId);
            if (wallet == null)
            {
                throw new Exception("Wallet record not found.");
            }

            return await _walletRepository.SoftDelete(wallet) > 0;
        }
    }
}
