using Application.DTOs.AuthResponse;
using Application.DTOs.UserDTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MobileWalletAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login User
    /// </summary>
    /// <param name="userlogin"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userlogin)
    {

        if (userlogin == null || string.IsNullOrWhiteSpace(userlogin.Email) || string.IsNullOrWhiteSpace(userlogin.HashPassword))
        {
            return BadRequest("Invalid login request.");
        }

        var result = await _authService.LoginUserAsync(userlogin);

        if (result.StatusCode == 200)
        {
            return Ok(new
            {
                accessToken = result.AccessToken,
                message = "Login successful."
            });
        }
        else if (result.StatusCode == 400)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return StatusCode(500, "An error occurred while processing your request.");
    }

    /// <summary>
    /// Refresh Token Call
    /// </summary>
    /// <param name="tokenRequest"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] AuthResponse tokenRequest)
    {

        var refreshToken = tokenRequest?.RefreshToken ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized("Invalid refresh token.");

        var result = await _authService.RefreshToken(new AuthResponse { RefreshToken = refreshToken });

        if (result == null)
            return Unauthorized(new { message = "Refresh token expired or invalid." });

        return Ok(result);
    }
}