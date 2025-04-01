using Application.DTOs.UserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.IRepository;
using Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<User, string> _userRepository;
        private readonly IGenericRepository<Wallet, string> _walletRepository;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUnit unit, IUserRepo userRepo, IMapper mapper)
        {
            _unit = unit;
            _userRepository = _unit.GetRepository<User, string>();
            _walletRepository = _unit.GetRepository<Wallet, string>();
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<int> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userRegistrationDto.Email))
                {
                    throw new ArgumentException("Email cannot be empty.", nameof(userRegistrationDto.Email));
                }

                // Check if user already exists
                var existingUser = await _userRepo.FindByEmailAsync(userRegistrationDto.Email);
                if (existingUser != null)
                {
                    Console.WriteLine("Email is already in use.");
                    return -1; // Return -1 to indicate failure
                }

                // Create new user
                var user = _mapper.Map<User>(userRegistrationDto);
                user.Id = Guid.NewGuid().ToString().ToUpper();
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.HashPassword); // Fix variable name

                int userResult = await _userRepository.Add(user);
                if (userResult <= 0)
                {
                    Console.WriteLine("Failed to register user.");
                    return -1;
                }

                // Automatically create a wallet for the user
                var wallet = new Wallet
                {
                    Id = Guid.NewGuid().ToString(),
                    Balance = 0,
                    UserId = user.Id,
                    Name = user.Name,

                };

                int walletResult = await _walletRepository.Add(wallet);
                if (walletResult <= 0)
                {
                    Console.WriteLine("Failed to create wallet.");
                    return -1;
                }

                Console.WriteLine("User registered successfully with wallet.");
                return userResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                return -1;
            }
        }
        public async Task<int> CountAll()
        {
            return await _userRepository.Count();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll("");
        }

        public Task<int> SoftDelete(string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<UserWithWalletAndKYCDto>> GetAllUsersWithWalletAndKYC()
        {
            return await _userRepo.GetAllUsersWithWalletAndKYC();
        }
    }
}
