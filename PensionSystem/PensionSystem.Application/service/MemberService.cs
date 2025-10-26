using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;


namespace PensionSystem.Application.service;

public class MemberService
{
    private readonly IUnitofWork _uow;
    public MemberService(IUnitofWork uow) => _uow = uow;

    public async Task<Guid> CreateMemberAsync(CreateMemberDto dto)
    {
        var member = new Member(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, dto.PhoneNumber);
        await _uow.Members.AddAsync(member);
        await _uow.CommitAsync();
        return member.Id;
    }

    public async Task UpdateMemberAsync(Guid id, UpdateMemberDto dto)
    {
        var member = await _uow.Members.GetByIdAsync(id);
        if (member == null) throw new InvalidOperationException("Member not found");
        member.Update(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, dto.PhoneNumber);
        _uow.Members.Update(member);
        await _uow.CommitAsync();
    }

    public async Task SoftDeleteMemberAsync(Guid id)
    {
        var member = await _uow.Members.GetByIdAsync(id);
        if (member == null) throw new InvalidOperationException("Member not found");
        member.SoftDelete();
        _uow.Members.Update(member);
        await _uow.CommitAsync();
    }
}