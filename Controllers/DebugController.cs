using Microsoft.AspNetCore.Mvc;

namespace PraticaCargo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    [HttpGet("headers")]
    public IActionResult Headers()
    {
        var auth = Request.Headers.Authorization.ToString();
        return Ok(new { authorization = auth });
    }
}
