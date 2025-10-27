using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Models;

namespace PensionSystem.Domain.interfaces.IService;

public interface IMemberService
{
    Task<CustomResponse> CreateMemberAsync(CreateMemberDto dto);
    Task<CustomResponse> UpdateMemberAsync(Guid id, UpdateMemberDto dto);
    Task<CustomResponse> SoftDeleteMemberAsync(Guid id);
    Task<CustomResponse> GetAllMembersAsync();
    Task<CustomResponse> GetMemberAsync(Guid memberId);
}