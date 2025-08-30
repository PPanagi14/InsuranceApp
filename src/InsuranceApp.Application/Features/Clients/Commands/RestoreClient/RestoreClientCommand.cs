using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Application.Features.Clients.Commands.RestoreClient;

public record RestoreClientCommand(Guid Id) : IRequest<bool>;
