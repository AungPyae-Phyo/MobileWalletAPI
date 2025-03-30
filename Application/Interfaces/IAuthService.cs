using Application.DTOs.AuthResponse;
using Application.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginUserAsync(UserLoginDto userDto);

        Task<int> RefreshToken(AuthResponse tokenRequest);
    }
}
