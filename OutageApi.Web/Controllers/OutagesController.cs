using Microsoft.AspNetCore.Mvc;
using OutageApi.Application.Interfaces;
using OutageApi.Application.DTOs;

namespace OutageApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]

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