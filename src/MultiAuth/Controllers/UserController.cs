using MultiAuth.Auth;

namespace MultiAuth.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("dynamic-data")]
    [DynamicAuthorize(["Admin", "Manager"], "read")]
    public IActionResult GetDynamicData()
    {
        return Ok(new { Message = "You have access!. Via DynamicAuthorize" });
    }
    
    [HttpGet("azure-user")]
    [AuthorizeRoleAttribute(["Admin", "Manager"])]
    public IActionResult GetAzureData()
    {
        return Ok(new { Message = "You have access!. Via Azure User" });
    }
    
    [HttpGet("no-user")]
    public IActionResult GetSecureData()
    {
        return Ok(new { Message = "You have access!. No Auth Needed." });
    }
}