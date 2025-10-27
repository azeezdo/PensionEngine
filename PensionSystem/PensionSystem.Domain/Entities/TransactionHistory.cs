namespace PensionSystem.Domain.Entities;

public class TransactionHistory : BaseEntity
{
    public Guid EntityId { get; private set; }
    public string EntityType { get; private set; } = null!;
    public string Action { get; private set; } = null!;
    public string ChangesJson { get; private set; } = "{}";
    public DateTime OccurredAt { get; private set; } = DateTime.UtcNow;

    protected TransactionHistory() { }

    public TransactionHistory(Guid entityId, string entityType, string action, string changesJson)
    {
        EntityId = entityId;
        EntityType = entityType;
        Action = action;
        ChangesJson = changesJson;
        CreatedAt = DateTime.UtcNow;
    }
}