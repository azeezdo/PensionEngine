namespace PensionSystem.Domain.Entities;

public class Member : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateOnly DateOfBirth { get; private set; }
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    public Guid? EmployerId { get; private set; }

    private readonly List<Contribution> _contributions = new();
    public IReadOnlyCollection<Contribution> Contributions => _contributions.AsReadOnly();

    protected Member() { }

    public Member(string firstName, string lastName, DateOnly dateOfBirth, string email, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Update(string firstName, string lastName, DateOnly dateOfBirth, string email, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void SoftDelete() => IsDeleted = true;

    public void AddContribution(Contribution c) => _contributions.Add(c);
}