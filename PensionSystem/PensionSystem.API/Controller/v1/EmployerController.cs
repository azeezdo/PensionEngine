using Microsoft.AspNetCore.Mvc;
using global::PensionSystem.Domain.interfaces.IService;
using System.ComponentModel.DataAnnotations;
using System.Net;
using PensionSystem.Domain.Models;

namespace PensionSystem.API.Controller.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _service;
        public EmployerController(IEmployerService service)
        {
            _service = service;
        }
        [HttpPost("createemployer")]
        public async Task<IActionResult> CreateMember([FromBody] CreateEmployerDto Dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateEmployerAsync(Dto);
                if (result.ResponseCode == (int)HttpStatusCode.OK)
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
        [HttpPost("updateemployer")]
        public async Task<IActionResult> UpdateEmployer(Guid id, UpdateEmployerDto Dto)
        {

            if (ModelState.IsValid)
            {
                var result = await _service.UpdateEmployerAsync(id, Dto);
                if (result.ResponseCode == (int)HttpStatusCode.OK)
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
        [HttpGet("employer")]
        public async Task<IActionResult> GetEmployer([Required] Guid employerId)
        {
            var result = await _service.GetEmployerAsync(employerId);
            if (result.ResponseCode == (int)HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("employers")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await _service.GetAllEmployerAsync();
            if (result.ResponseCode == (int)HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteemployer")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var result = await _service.SoftDeleteEmployerAsync(id);
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
}
