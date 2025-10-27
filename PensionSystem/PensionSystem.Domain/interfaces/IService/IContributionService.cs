using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Models;

namespace PensionSystem.Domain.interfaces.IService;

public interface IContributionService
{
    Task<CustomResponse> AddContributionAsync(CreateContributionDto dto);
}