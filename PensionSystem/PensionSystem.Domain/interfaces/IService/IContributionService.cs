using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Domain.Models;

namespace PensionSystem.Domain.interfaces.IService;

public interface IContributionService
{
    Task<CustomResponse> AddContributionAsync(Contribution dto);
   


}