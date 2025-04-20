    using Application.DTOs.LimitFeesDTO;
using Application.Interfaces;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileWalletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LimitFeesController : ControllerBase
    {
        private readonly ILimitFeesService _limitFeesService;


        public LimitFeesController(ILimitFeesService limitFeesService)
        {
            _limitFeesService = limitFeesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLimitFees([FromBody] LimitFeesDTO limitFeesDto)
        {
            try
            {
                if (limitFeesDto == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid input data",
                        status = "error",
                        errors = new List<object>
                        {
                            new { field = "LimitFees", message = "Limit fees data is required" }
                        }
                    });
                }

                var result = await _limitFeesService.Create(limitFeesDto);

                return Ok(new
                {
                    message = "Limit fees created successfully",
                    status = "success",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to create limit fees",
                    status = "error",
                    errors = new List<object>
                    {
                        new { field = "LimitFees", message = ex.Message }
                    }
                });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLimitFees(string id, [FromBody] LimitFeesDTO limitFeesDto)
        {
            try
            {
                if (limitFeesDto == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid input data",
                        status = "error",
                        errors = new List<object>
                {
                    new { field = "LimitFees", message = "Limit fees data is required" }
                }
                    });
                }

                var result = await _limitFeesService.Update(id, limitFeesDto);

                return Ok(new
                {
                    message = "Limit fees updated successfully",
                    status = "success",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to update limit fees",
                    status = "error",
                    errors = new List<object>
            {
                new { field = "LimitFees", message = ex.Message }
            }
                });
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllLimitFees()
        {
            try
            {
                var result = await _limitFeesService.GetAll();

                return Ok(new
                {
                    message = "Limit fees retrieved successfully",
                    status = "success",
                    count = result?.Count() ?? 0,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to retrieve limit fees",
                    status = "error",
                    errors = new List<object>
                    {
                        new { field = "LimitFees", message = ex.Message }
                    }
                });
            }
        }
    }
}
