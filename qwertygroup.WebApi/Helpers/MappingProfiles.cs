using AutoMapper;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.WebApi.Dtos;

namespace CompAssignmnetSDPSecurity.WebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Id, o => o.MapFrom(p => p.Id))
                .ForMember(d => d.Name, o => o.MapFrom(p => p.Name));;
        }
    }
}