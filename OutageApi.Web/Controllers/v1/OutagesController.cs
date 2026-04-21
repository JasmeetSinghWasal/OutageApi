using Microsoft.AspNetCore.Mvc;
using OutageApi.Application.Interfaces;
using OutageApi.Application.DTOs;
using Asp.Versioning;

namespace OutageApi.Web.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OutagesController : ControllerBase
{
    private readonly IOutageService _outageService;
    private readonly ILogger<OutagesController> _logger;

    public OutagesController(IOutageService outageService, ILogger<OutagesController> logger)
    {
        _outageService = outageService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OutageResponse>> All()
    {

        _logger.LogInformation("Getting all outages");
        // Testing log config from appsetting.json //Working, commented for info only
        // _logger.LogDebug("Calling OutageService.All() to retrieve all outages");
        // _logger.LogError("This is a test error log for demonstration purposes in V1 - get all outages endpoint");

        IEnumerable<OutageResponse>? outages = _outageService.All();
        return Ok(outages);
    }
}