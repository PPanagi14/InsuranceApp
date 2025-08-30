
namespace InsuranceApp.Domain.Entities;

public class Role : BaseEntity
{
    public Guid RoleTypeId { get; set; }
    public RoleTypeEntity RoleType { get; set; } = null!;

    // many-to-many
    public ICollection<User> Users { get; set; } = new List<User>();
}
