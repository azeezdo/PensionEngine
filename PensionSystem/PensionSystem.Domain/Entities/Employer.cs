namespace PensionSystem.Domain.Entities;

public class Employer : BaseEntity
{
    public string CompanyName { get; private set; } = null!;
    public string RegistrationNumber { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; } = true;
    protected Employer() { }

    public Employer(string companyName, string registrationNumber)
    {
        CompanyName = companyName;
        RegistrationNumber = registrationNumber;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string companyName, string registrationNumber)
    {
        CompanyName = companyName;
        RegistrationNumber = registrationNumber;
        CreatedAt = DateTime.UtcNow;
    }
    public void SoftDelete() => IsDeleted = true;
    public void Deactivate() => IsActive = false;
}