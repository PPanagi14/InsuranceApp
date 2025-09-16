using InsuranceApp.Domain.Entities;

public class PolicySummaryDto
{
    public Guid Id { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public PolicyType PolicyType { get; set; }
    public PolicyStatus Status { get; set; }
    public decimal PremiumAmount { get; set; }
    public string Currency { get; set; } = "EUR";
}
