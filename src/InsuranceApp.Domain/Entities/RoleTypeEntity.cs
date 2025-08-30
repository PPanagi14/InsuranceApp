namespace InsuranceApp.Domain.Entities;

public class RoleTypeEntity : BaseEntity
{
    public string Code { get; set; } = string.Empty;       // e.g. "ADMIN", "BROKER"
    public string Description { get; set; } = string.Empty; // e.g. "System administrator"

    public ICollection<Role> Roles { get; set; } = new List<Role>();
}
