namespace InsuranceApp.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime? DeletedAtUtc { get; set; }

    // 🔹 Auditing fields
    public Guid? CreatedBy { get; set; }      // user who created
    public Guid? UpdatedBy { get; set; }      // last user who updated
    public Guid? DeletedBy { get; set; }      // user who soft-deleted

}
