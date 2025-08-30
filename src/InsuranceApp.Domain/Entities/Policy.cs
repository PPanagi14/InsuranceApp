namespace InsuranceApp.Domain.Entities;

public enum PolicyType { Auto, Home, Life, Health, Travel, Business, Other }
public enum PolicyStatus { Active, Pending, Expired, Cancelled }

public class Policy : BaseEntity
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public string Insurer { get; set; } = string.Empty;
    public PolicyType PolicyType { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal PremiumAmount { get; set; }
    public string Currency { get; set; } = "EUR";

    public PolicyStatus Status { get; set; } = PolicyStatus.Pending;
}
