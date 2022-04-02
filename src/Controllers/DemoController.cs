using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
        => _logger = logger;

    [HttpGet("call1")]
    public IActionResult GetCall1()
    {
        _logger.LogInformation($"DEMO > {nameof(GetCall1)}");

        return Ok(nameof(GetCall1));
    }

    [HttpGet("call2")]
    public IActionResult GetCall2()
    {
        _logger.LogInformation($"DEMO > {nameof(GetCall2)}");

        return Ok(nameof(GetCall2));
    }
}
