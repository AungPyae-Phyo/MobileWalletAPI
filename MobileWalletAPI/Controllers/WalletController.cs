using Application.DTOs.WalletDTO;
using Application.Interfaces;
using Domain.Contracts;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileWalletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IWalletHistoryService  _walletHistoryService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("create")]
        //public async Task<IActionResult> CreateWallet([FromBody] WalletDTO walletDto)
        //{
        //    try
        //    {
        //        var result = await _walletService.Create(walletDto);

        //        return Ok(new
        //        {
        //            message = "Wallet created successfully",
        //            status = "success",
        //            data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            message = "Failed to create wallet",
        //            status = "error",
        //            errors = new List<object> { new { field = "Wallet", message = ex.Message } }
        //        });
        //    }
        //}

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWallets()
        {
            try
            {
                var result = await _walletService.GetAll();

                return Ok(new
                {
                    message = "Wallets retrieved successfully",
                    status = "success",
                    count = result.Count(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to retrieve wallets",
                    status = "error",
                    errors = new List<object> { new { field = "Wallet", message = ex.Message } }
                });
            }
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetWallet(string userId)
        {
            try
            {
                var result = await _walletService.Get(userId);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Wallet not found",
                        status = "error",
                        errors = new List<object> { new { field = "userId", message = "No wallet associated with this user" } }
                    });
                }

                return Ok(new
                {
                    message = "Wallet retrieved successfully",
                    status = "success",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to retrieve wallet",
                    status = "error",
                    errors = new List<object> { new { field = "Wallet", message = ex.Message } }
                });
            }
        }
        [HttpGet("history/{walletId}")]
        public async Task<IActionResult> GetWalletHistory(string walletId)
        {
            try
            {
                var history = await _walletService.GetWalletHistory(walletId);

                return Ok(new
                {
                    message = "History retrieved successfully",
                    status = "success",
                    count = history.Count(),
                    data = history
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WalletController] ERROR: {ex.Message}");
                return StatusCode(500, new
                {
                    message = "Failed to retrieve wallet history",
                    status = "error",
                    errors = new List<object> {
                new { field = "WalletHistory", message = ex.Message }
            }
                });
            }
        }



        [HttpPut("update-balance")]
        public async Task<IActionResult> UpdateBalance([FromBody] WalletDTO walletDto)
        {
            try
            {
                var updatedWallet = await _walletService.UpdateBalance(walletDto.WalletId, walletDto.Balance);

                return Ok(new
                {
                    message = "Wallet balance updated successfully",
                    status = "success",
                    data = updatedWallet
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("UPDATE BALANCE ERROR: " + ex.Message);
                return StatusCode(500, new
                {
                    message = "Failed to update balance",
                    status = "error",
                    errors = new List<object> { new { field = "Balance", message = ex.Message } }
                });
            }
        }


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteWallet(string userId)
        {
            try
            {
                var result = await _walletService.SoftDelete(userId);

                if (!result)
                {
                    return NotFound(new
                    {
                        message = "Wallet not found or already deleted",
                        status = "error",
                        errors = new List<object> { new { field = "userId", message = "No wallet found for deletion" } }
                    });
                }

                return Ok(new
                {
                    message = "Wallet deleted successfully",
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to delete wallet",
                    status = "error",
                    errors = new List<object> { new { field = "Wallet", message = ex.Message } }
                });
            }
        }
    }
}
