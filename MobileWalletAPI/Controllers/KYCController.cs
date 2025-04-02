using Application.DTOs.KYCDTO;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromForm] KYCDTO kycDTO)
        {
            try
            {
                var result = await _kycService.Create(kycDTO);
                return CreatedAtAction(nameof(GetById), new { id = kycDTO.UserID }, new
                {
                    message = "KYC submitted successfully",
                    status = "success",
                    data = new { kycId = kycDTO.UserID }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Failed to submit KYC",
                    status = "error",
                    errors = new List<object> { new { field = "KYC", message = ex.Message } }
                });
            }
        }

        /// <summary>
        /// Get all KYC records.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllKycs()
        {
            var kycs = await _kycService.GetAll();

            if (kycs == null || !kycs.Any())
            {
                return NotFound(new
                {
                    message = "No KYC records found",
                    status = "error",
                    count = 0,
                    errors = new List<object> { new { field = "KYC", message = "No records exist in the database." } }
                });
            }

            return Ok(new
            {
                message = "KYC records retrieved successfully",
                status = "success",
                count = kycs.Count(),
                data = kycs
            });
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
                return Ok(new
                {
                    message = "KYC record retrieved successfully",
                    status = "success",
                    data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = "KYC record not found",
                    status = "error",
                    errors = new List<object> { new { field = "ID", message = ex.Message } }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "An error occurred",
                    status = "error",
                    errors = new List<object> { new { field = "General", message = ex.Message } }
                });
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
                return NotFound(new
                {
                    message = "KYC record not found or already deleted",
                    status = "error",
                    errors = new List<object> { new { field = "ID", message = "Record does not exist or is already deleted." } }
                });
            }

            return Ok(new
            {
                message = "KYC record deleted successfully",
                status = "success"
            });
        }

        /// <summary>
        /// Update KYC record.
        /// </summary>
        [HttpPut("update-status/{userId}")]
        public async Task<IActionResult> UpdateStatus([FromBody] KYCStatusDTO kycStatusDTO, string userId)
        {
            try
            {
                // Check if userId in the route matches userId in the DTO
                if (userId != kycStatusDTO.UserId)
                {
                    return BadRequest(new
                    {
                        message = "User ID mismatch.",
                        status = "error",
                        errors = new List<object> { new { field = "UserId", message = "User ID in route does not match User ID in the body." } }
                    });
                }

                // Call the service to update the status
                var result = await _kycService.UpdateStatus(kycStatusDTO);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        message = "KYC record not found",
                        status = "error",   
                        errors = new List<object> { new { field = "ID", message = "No matching record found to update." } }
                    });
                }

                return Ok(new
                {
                    message = "KYC status updated successfully",
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Failed to update KYC status",
                    status = "error",
                    errors = new List<object> { new { field = "General", message = ex.Message } }
                });
            }
        }

    }
}
