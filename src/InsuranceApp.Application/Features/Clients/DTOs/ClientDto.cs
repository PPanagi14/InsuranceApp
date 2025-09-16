using InsuranceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Clients.DTOs;

public record ClientDto(
    Guid Id,
    ClientType Type,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string Email,
    string PhoneMobile,
    string? City,
    int PoliciesCount
);