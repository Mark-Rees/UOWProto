using AutoMapper;
using ProtoType.Core.Dto;
using ProtoType.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Api.Mappings
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<Fixture, FixtureDto>()
                .ReverseMap();

            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom((src, dest) =>
                {
                    return src.Players.Select(x => x.Name);
                }))
                .ReverseMap();

        }
    }
}
