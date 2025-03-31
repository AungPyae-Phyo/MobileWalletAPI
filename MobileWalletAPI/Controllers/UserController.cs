using Application.DTOs.UserDTO;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileWalletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserRegistrationDto userRegistrationDto)
        {
            int response = await _service.RegisterUserAsync(userRegistrationDto);
            return Created("", new { result = (response > 0) });

        }
       
    }
}

