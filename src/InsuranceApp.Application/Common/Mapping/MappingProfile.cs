using AutoMapper;
using InsuranceApp.Application.Features.Clients.DTOs;
using InsuranceApp.Application.Features.Policies.DTOs;
using InsuranceApp.Domain.Entities;

namespace InsuranceApp.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Client, ClientDetailDto>();

        CreateMap<Policy, PolicyDto>();
        CreateMap<Policy, PolicyDetailDto>();
    }
}
