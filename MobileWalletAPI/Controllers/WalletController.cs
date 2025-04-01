using Application.DTOs.WalletDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MobileWalletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWallet([FromBody] WalletDTO walletDto)
        {
            try
            {
                var result = await _walletService.Create(walletDto);
                if (result > 0)
                {
                    return Ok(new { message = "Wallet created successfully" });
                }
                return BadRequest(new { message = "Failed to create wallet" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetWallet(string userId)
        {
            try
            {
                var wallet = await _walletService.Get(userId);
                if (wallet != null)
                {
                    return Ok(wallet);
                }
                return NotFound(new { message = "Wallet not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("update-balance")]
        public async Task<IActionResult> UpdateBalance([FromBody] WalletDTO walletDto)
        {
            try
            {
                var result = await _walletService.UpdateBalance(walletDto.UserId, walletDto.Balance);
                if (result > 0)
                {
                    return Ok(new
                    {
                        message = "Wallet balance updated successfully",
                        status = "success",
                        data = new { userId = walletDto.UserId, balance = walletDto.Balance }
                    });
                }
                return BadRequest(new
                {
                    message = "Failed to update wallet balance",
                    status = "error",
                    errors = new List<object> { new { field = "balance", message = "Could not process update." } }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred",
                    status = "error",
                    errors = new List<object> { new { field = "general", message = ex.Message } }
                });
            }
        }


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteWallet(string userId)
        {
            try
            {
                var result = await _walletService.SoftDelete(userId);
                if (result)
                {
                    return Ok(new { message = "Wallet deleted successfully" });
                }
                return BadRequest(new { message = "Failed to delete wallet" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
