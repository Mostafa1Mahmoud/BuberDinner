using BuberDinner.Application.Services.Authentication;
using BuberDinnner.Contracts.Authntication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        AuthenticationResult authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        AuthenticationResponse authResponse = new AuthenticationResponse(authResult.Id, authResult.FirstName, authResult.LastName, authResult.Email, authResult.Token);

        return Ok(authResponse);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        AuthenticationResult authResult = _authenticationService.Login(request.Email, request.Password);

        AuthenticationResponse authResponse = new AuthenticationResponse(authResult.Id, authResult.FirstName, authResult.LastName, authResult.Email, authResult.Token);

        return Ok(authResponse);
    }
}