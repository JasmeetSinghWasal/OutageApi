using Microsoft.AspNetCore.Mvc;
using OutageApi.Application.Interfaces;
using OutageApi.Application.DTOs;
using Asp.Versioning;

namespace OutageApi.Web.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}[controller]")]
[ApiVersion("1.0")]
public class OutagesController : ControllerBase
{
    private readonly IOutageService _outageService;

    public OutagesController(IOutageService outageService)
    {
        _outageService = outageService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OutageResponse>> All()
    {
        IEnumerable<OutageResponse>? outages = _outageService.All();
        return Ok(outages);
    }
}