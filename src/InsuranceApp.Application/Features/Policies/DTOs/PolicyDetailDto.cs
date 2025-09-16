using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Policies.DTOs;

public class PolicyDetailDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Insurer { get; set; } = string.Empty;
    public PolicyType PolicyType { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PremiumAmount { get; set; }
    public string Currency { get; set; } = "EUR";
    public PolicyStatus Status { get; set; }

    // Extra fields
    public decimal? CoverageAmount { get; set; }
    public PaymentFrequency PaymentFrequency { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal? BrokerCommission { get; set; }
    public DateTime? RenewalDate { get; set; }
}
