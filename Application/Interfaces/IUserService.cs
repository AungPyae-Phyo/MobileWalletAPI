using Application.DTOs.UserDTO;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<IEnumerable<UserWithWalletAndKYCDto>> GetAllUsersWithWalletAndKYC();
        Task<IEnumerable<User>> GetAll();
        Task<int> CountAll();
        Task<int> SoftDelete(string userId);
    }
}
