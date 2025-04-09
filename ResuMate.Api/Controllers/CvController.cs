using Microsoft.AspNetCore.Mvc;

namespace ResuMate.Api.Controllers;


[ApiController]
[Route("api/")]
public class CvController : ControllerBase
{



    [HttpGet("hello")]
    public IActionResult GetHello()
    {
        return Ok(new { message = "API fungerar! 🚀" });
    }

    [HttpPost("echo")]
    public IActionResult EchoMessage([FromBody] TestRequest request)
    {
        return Ok(new { message = $"Du sa: {request.Message}" });
    }

    // GET: api/greeting/sayhello
    [HttpGet("sayhello")]
    public IActionResult SayHello([FromQuery] string name = "Guest")
    {
        return Ok(new { message = $"Hello, {name}! Welcome to our API." });
    }

}

public class TestRequest
{
    public string? Message { get; set; }
}
    

    
