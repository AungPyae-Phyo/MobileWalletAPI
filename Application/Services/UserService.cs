using Application.DTOs.UserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.IRepository;
using Infrastructure.UnitOfWork;

namespace Application.Services;
public class UserService: IUserService
{
    private readonly IUnit _unit;
    private readonly IGenericRepository<User, string> _genericRepository;
    private readonly IUserRepo _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUnit unit, IUserRepo userRepository, IMapper mapper)
    {
        _unit = unit;
        _genericRepository = _unit.GetRepository<User, string>();
        _userRepository = userRepository;
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

            var existingUser = await _userRepository.FindByEmailAsync(userRegistrationDto.Email);

            if (existingUser != null)
            {
                Console.WriteLine("Email is already in use.");
                return -1;
            }

            var user = _mapper.Map<User>(userRegistrationDto);
            user.Id = Guid.NewGuid().ToString().ToUpper();

            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.HashPassword);

            int result = await _genericRepository.Add(user);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering user: {ex.Message}");
            return -1;
        }
    }




    //public async Task<User> GetById(string id)
    //{
    //    var user = await _genericRepository.Get(id);
    //    if (user == null)
    //        throw new Exception("Member record does not exist.");
    //    return user;
    //}


}
