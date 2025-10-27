namespace PensionSystem.Domain.Models;

public class GetMembersResponse
{
    public Guid MemberId { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } 
    public string? PhoneNumber { get; set; }
    public Guid EmployerId  { get; set; }
}

public class GetEmployersResponse
{
    public Guid EmployerId { get; set; }
    public string CompanyName { get; set; }
    public string RegistrationNumber { get; set; }
    
}