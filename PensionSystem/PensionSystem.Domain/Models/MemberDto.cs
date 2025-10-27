namespace PensionSystem.Application.DTOs;

public record MemberDto(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth, string Email, string? PhoneNumber);

public record CreateMemberDto(string FirstName, string LastName, DateOnly DateOfBirth, string Email, string? PhoneNumber);

public record UpdateMemberDto(string FirstName, string LastName, DateOnly DateOfBirth, string Email, string? PhoneNumber);