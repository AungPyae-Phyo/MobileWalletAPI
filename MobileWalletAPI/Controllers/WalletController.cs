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
    }
}
