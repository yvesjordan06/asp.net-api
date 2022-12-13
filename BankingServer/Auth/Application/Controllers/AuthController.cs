using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingServer.Auth.Application.DTO;
using BankingServer.Auth.Domain.Services;
using BankingServer.Core.Entities;
using BankingServer.Core.Exceptions;

namespace BankingServer.Auth.Application.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    //Login Route
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 201)]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(typeof(BankError), 400)]
    [ProducesResponseType(typeof(BankError), 404)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var authToken = await _authService.LoginAsync(request.Email, request.Password);

            //Transform the token into a response
            var response = AuthResponse.FromAuthTokenModel(authToken);


            return Ok(response);
        }
        catch (BankException e)
        {
            return e.ToActionResult();
        }
    }

    //Register Route
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), 201)]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(typeof(BankError), 400)]
    [ProducesResponseType(typeof(BankError), 404)]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.RegisterAsync(request.Email, request.Password);

            return Ok(response);
        }
        catch (BankException e)
        {
            return e.ToActionResult();
        }
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> OnPostUpload( IFormFile image)
    {
   
        
        //Converting the file to a stream
        var stream = image.OpenReadStream();

        // Apple___Apple_Scab : 100%
        var result =  await _authService.Predict(stream);
        var tag = result.Split(":")[0];
        var confidence = result.Split(":")[1].Trim();
        var tree = _authService.Translate(tag.Split("___")[0].Replace("_", " ").Trim(), "fr").Result;
        var disease = _authService.Translate(tag.Split("___")[1].Replace("_", " ").Trim(), "fr").Result;
        
        
        return Ok(new {tree, disease, confidence});

    }
    
    [HttpPost("question")]
    public async Task<IActionResult> AskQuestion(Question question)
    {
        var result = await _authService.AskQuestion(question.question);
        return Ok(result);

    }
}

//define a simple type to get the request body in the form {question: "question"}
public struct Question
{
    public string question { get; set; }
}