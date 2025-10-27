using System.Net;
using Microsoft.AspNetCore.Mvc;
using PensionSystem.Application.DTOs;
using PensionSystem.Domain.interfaces.IService;

namespace PensionSystem.API.Controller.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ContributionController : ControllerBase
{
    private readonly IContributionService _contributionService;
    public ContributionController(IContributionService  contributionService)
    {
        _contributionService = contributionService;
    }
    [HttpPost("addcontribution")]
    public async Task<IActionResult> AddContribution( [FromBody]CreateContributionDto Dto)
    {
        if(ModelState.IsValid)
        {
            var result= await _contributionService.AddContributionAsync(Dto);
            if(result.ResponseCode == (int)HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }    
        }
        var errors = ModelState.Values.ToList();
        return BadRequest(ModelState);
    }
}