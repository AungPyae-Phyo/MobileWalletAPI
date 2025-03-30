using Application.DTOs.KYCDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MobileWalletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KYCController : ControllerBase
    {
        private readonly IKYCService _kycService;

        public KYCController(IKYCService kycService)
        {
            _kycService = kycService;
        }

        /// <summary>
        /// Submit KYC registration (Only for registered users).
        /// </summary>
        [HttpPost("kyc-register")]
        public async Task<IActionResult> Post([FromForm] KYCDTO kycDTO)
        {
            try
            {
                var result = await _kycService.Create(kycDTO);
                return CreatedAtAction(nameof(GetById), new { id = kycDTO.UserID }, new { message = "KYC submitted successfully", kycId = kycDTO.UserID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all KYC records.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _kycService.GetAll();
            if (result == null)
            {
                return NotFound(new { message = "No KYC records found" });
            }
            return Ok(result);
        }

        /// <summary>
        /// Get KYC record by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _kycService.GetById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred", details = ex.Message });
            }
        }


        /// <summary>
        /// Soft delete a KYC record.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _kycService.SoftDelete(id);
            if (!success)
            {
                return NotFound(new { message = "KYC record not found or already deleted" });
            }
            return Ok(new { message = "KYC record deleted successfully" });
        }

        /// <summary>
        /// Update KYC record.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] KYCDTO kycDTO)
        {
            try
            {
                var result = await _kycService.Update(kycDTO);
                if (result == 0)
                {
                    return NotFound(new { message = "KYC record not found" });
                }
                return Ok(new { message = "KYC updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
