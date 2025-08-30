namespace InsuranceApp.Domain.Entities;

public abstract class BaseEntity
{
public Guid Id { get; set; } = Guid.NewGuid();
public DateTime CreatedAtUtc { get; set; } 
public DateTime UpdatedAtUtc { get; set; } 
public DateTime? DeletedAtUtc { get; set; }
}


