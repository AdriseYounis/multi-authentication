using Microsoft.AspNetCore.Mvc;
using MultiAuth.TokenGenerator;

namespace MultiAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JwtController : ControllerBase
{
    [HttpGet("generate-tokens")]
    public IActionResult GenerateTokens()
    {
        var azureUserToken = JwtTokenGenerator.GenerateAzureUserJwt();
        var signInUserToken = JwtTokenGenerator.GenerateSignInUserJwt();

        return Ok(new
        {
            AzureUserToken = azureUserToken,
            SignInUserToken = signInUserToken
        });
    }
}