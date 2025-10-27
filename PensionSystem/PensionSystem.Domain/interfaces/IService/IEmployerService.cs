using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionSystem.Domain.interfaces.IService
{
    public interface IEmployerService
    {
        Task<CustomResponse> CreateEmployerAsync(CreateEmployerDto dto);
        Task<CustomResponse> UpdateEmployerAsync(Guid id, UpdateEmployerDto dto);
        Task<CustomResponse> SoftDeleteEmployerAsync(Guid id);
        Task<CustomResponse> GetAllEmployerAsync();
        Task<CustomResponse> GetEmployerAsync(Guid employerId);
    }
}
