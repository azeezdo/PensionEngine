using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Domain.Models;


namespace PensionSystem.Application.service;

public class MemberService : IMemberService
{
    private readonly IUnitofWork _uow;
    public MemberService(IUnitofWork uow) => _uow = uow;

    public async Task<CustomResponse> CreateMemberAsync(CreateMemberDto dto)
    {
        CustomResponse res = null;
        try
        {
            var existMember = await _uow.Members.GetByExpressionAsync(x => x.Email == dto.Email);
            if (existMember == null)
            {
                var member = new Member(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, dto.PhoneNumber);
                await _uow.Members.AddAsync(member);
                await _uow.CommitAsync();
                res = new CustomResponse(200,  "Member Successfully Created", null);
            }
            res = new CustomResponse(404, $"Member with email {dto.Email} not found", null);
          
        }
        catch (Exception e)
        {
           
        }
        return res;
    }
    public async Task<CustomResponse> UpdateMemberAsync(Guid id, UpdateMemberDto dto)
    {
        CustomResponse res = null;
        try
        {
            var member = await _uow.Members.GetByIdAsync(id);
            if (member == null)
            {
                res = new CustomResponse(404, $"Member with id {id} not found", null);
            }
            member.Update(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, dto.PhoneNumber);
            _uow.Members.Update(member);
            await _uow.CommitAsync();
            res = new CustomResponse(200,  "Member Successfully Updated", null);
        }
        catch (Exception e)
        {
           
        }
        return res;
    }

    public async Task<CustomResponse> SoftDeleteMemberAsync(Guid id)
    {
        CustomResponse res = null;
        try
        {
            var member = await _uow.Members.GetByIdAsync(id);
            if (member == null)
            {
                res =  new CustomResponse(404, $"Member with id {id} not found", null);
            }
            member.SoftDelete();
            _uow.Members.Update(member);
            await _uow.CommitAsync();
            res = new CustomResponse(200,  "Member Successfully Deleted", null);
        }
        catch (Exception e)
        {
            
        }

        return res;
    }
    public async Task<CustomResponse> GetAllMembersAsync()
    {
        CustomResponse res = null;
        IEnumerable<Member> members = null;
        var memberResponse = new List<GetMembersResponse>();
        
        try
        {
             members = await _uow.Members.GetAllAsync();
            if (members != null)
            {
                foreach (var member in members)
                {
                    memberResponse.Add(new GetMembersResponse
                    {
                     MemberId   = member.Id,  
                     FirstName = member.FirstName,
                     LastName = member.LastName,
                     DateOfBirth = member.DateOfBirth,
                     Email = member.Email,
                     PhoneNumber = member.PhoneNumber,
                    });
                }
                res = new CustomResponse(200,  "Member Successfully Retrieved", memberResponse.OrderBy(x => x.MemberId));
            }
            else
            {
                res = new CustomResponse(404, $"Members not found", memberResponse);
            }

        }
        catch (Exception e)
        {
           res = new CustomResponse(404, $"Members not found", null);
        }
        return res;
    }
    public async Task<CustomResponse> GetMemberAsync(Guid memberId)
    {
        CustomResponse res = null;
        try
        {
            var member = await _uow.Members.GetByIdAsync(memberId);
            if (member != null)
            {
                var result = new GetMembersResponse
                {
                    MemberId = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    DateOfBirth = member.DateOfBirth,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber
                };
                res = new CustomResponse(200, "Member Successfully Retrieved", member);
            }
            else
            {
                res = new CustomResponse(404, $"Members not found", member);
            }
        }
        catch (Exception e)
        {
            res = new CustomResponse(404, $"Members not found", null);
        }
        return res;
    }
}