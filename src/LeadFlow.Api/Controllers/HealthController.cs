using Microsoft.AspNetCore.Mvc;

namespace LeadFlow.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}

    