using Application.DTOs.UserDTO;
using Application.Interfaces;
using Application.Services;
using Domain.Contracts;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAll();
            if (users == null || !users.Any())
            {
                return NotFound(new
                {
                    message = "No users found",
                    status = "error",
                    count = 0,
                    data = new List<object>()
                });
            }
            return Ok(new
            {
                message = "Users fetched successfully",
                status = "success",
                count = users.Count(),
                data = users
            });
        }


        [HttpGet("all-with-wallet-kyc")]
        public async Task<IActionResult> GetAllUsersWithWalletAndKYC()
        {
            var usersWithDetails = await _service.GetAllUsersWithWalletAndKYC();
            if (usersWithDetails == null || !usersWithDetails.Any())
            {
                return NotFound(new
                {
                    message = "No users found with wallet and KYC details",
                    status = "error",
                    count = 0,
                    data = new List<object>()
                });
            }
            return Ok(new
            {
                message = "Users with wallet and KYC details fetched successfully",
                status = "success",
                count = usersWithDetails.Count(),
                data = usersWithDetails
            });
        }

    }
}

