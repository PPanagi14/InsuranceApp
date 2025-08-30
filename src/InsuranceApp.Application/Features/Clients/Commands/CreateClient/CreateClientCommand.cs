using InsuranceApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Clients.Commands.CreateClient;

public record CreateClientCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? City,
    string? CompanyName,
    ClientType Type = ClientType.Person
) : IRequest<Guid>;