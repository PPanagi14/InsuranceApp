using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Features.Clients.DTOs;

public class ClientDetailDto
{
    public Guid Id { get; set; }
    public ClientType Type { get; set; }

    // Person
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // Company
    public string? CompanyName { get; set; }
    public string? VatNumber { get; set; }

    // Contact
    public string Email { get; set; } = string.Empty;
    public string PhoneMobile { get; set; } = string.Empty;
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    // Notes
    public string? Notes { get; set; }
    public DateTime? CreatedAtUtc { get; set; }

    public string Status { get; set; } = "Active"; // computed from policies
    public decimal TotalPremium { get; set; }      // sum of all policy premiums

    public int? PoliciesCount {get; set; } 

    public List<PolicySummaryDto> Policies { get; set; } = [];
}
