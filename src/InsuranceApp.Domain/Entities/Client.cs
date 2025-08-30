namespace InsuranceApp.Domain.Entities;

public enum ClientType
{
    Person = 1,
    Company = 2
}

public class Client : BaseEntity
{
    public ClientType Type { get; set; }

    // Person
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Company
    public string? CompanyName { get; set; }

    // Contact
    public string Email { get; set; } = string.Empty;
    public string PhoneMobile { get; set; } = string.Empty;
    public string? City { get; set; }

    // Navigation
    public ICollection<Policy> Policies { get; set; } = [];
}
