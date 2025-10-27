using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PensionSystem.Domain.interfaces.IService;

namespace PensionSystem.API.Controller.v1;

public class BenefitController : ControllerBase
{
    private readonly IBenefitService _benefitService;
    public BenefitController(IBenefitService   benefitService)
    {
        _benefitService = benefitService;
    }
    [HttpGet("beneffit")]
    public async Task<IActionResult> GetMembers([Required] Guid memberId)
    {
        var result = await _benefitService.CalculateBenefitAsync(memberId);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}