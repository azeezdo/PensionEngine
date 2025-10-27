using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PensionSystem.Application.DTOs;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Domain.Models;

namespace PensionSystem.API.Controller.v1;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _service;
    public MemberController(IMemberService  service)
    {
        _service = service;
    }
    [HttpPost("createmember")]
    public async Task<IActionResult> CreateMember( [FromBody]CreateMemberDto Dto)
    {
        if(ModelState.IsValid)
        {
            var result= await _service.CreateMemberAsync(Dto);
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
    [HttpPost("updatemember")]
    public async Task<IActionResult> UpdateMember( Guid id, UpdateMemberDto Dto)
    {
            
        if(ModelState.IsValid)
        {
            var result= await _service.UpdateMemberAsync(id,Dto);
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
    [HttpGet("member")]
    public async Task<IActionResult> GetMembers([Required] Guid memberId)
    {
        var result = await _service.GetMemberAsync(memberId);
        if (result.ResponseCode == (int)HttpStatusCode.OK)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    [HttpGet("members")]
    public async Task<IActionResult> GetMembers()
    {
        var result = await _service.GetAllMembersAsync();
        if (result.ResponseCode == (int)HttpStatusCode.OK)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    [HttpDelete("deletemember")]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        var result = await _service.SoftDeleteMemberAsync(id);
        if (result.ResponseCode == (int)HttpStatusCode.OK)
        {
            return StatusCode(result.ResponseCode, result);

        }
        else
        {
            return BadRequest(result);
        }

    }
}