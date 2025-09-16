using InsuranceApp.Application.Features.Clients.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Clients.Querries.GetAllClientsWithDetails;

public class GetAllClientsWithDetailsQuery:IRequest<IReadOnlyList<ClientDetailDto>>;
