namespace OutageApi.Web.Controllers.v1;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OutageApi.Domain.Entities;
using OutageApi.Application.DTOs;
using OutageApi.Application.Interfaces;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase{
    
    private ILogger<AuthController> _logger;
    private  IUserService _userAuth;
    public AuthController(ILogger<AuthController> logger, IUserService userAuth)
    {
        _logger = logger;
        _userAuth = userAuth;
    }

    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login([FromBody] LoginRequest request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Console.WriteLine($"Received login request for username: {request.Username} with {request.Password}");

        var isAuthenticated = await _userAuth.AuthenticateUserAsync(request.Username, request.Password);
        // _logger.LogInformation($"Received login request for username: {request.Username}");
        return Ok(new {
            Message = "Login successful",
            isValid = isAuthenticated
        });
    }

}