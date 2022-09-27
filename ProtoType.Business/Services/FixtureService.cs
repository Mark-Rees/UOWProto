using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProtoType.Core.Dto;
using ProtoType.Core.Entities;
using ProtoType.Core.Enums;
using ProtoType.Core.Interfaces.Repositories;
using ProtoType.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Business.Services
{
    public class FixtureService : Service, IFixtureService
    {
        private readonly IFixtureRepository _fixtureRepository;

        public FixtureService(IMemoryCache cache, IMapper mapper, IFixtureRepository fixtureRepository) : base(cache, mapper)
        {
            _fixtureRepository = fixtureRepository;
        }

        public async Task<FixtureDto> Get(int id)
        {
            if (!Cache.TryGetValue($"GetFixture-{id}", out Fixture fixture))
            {
                fixture = await _fixtureRepository.Get(id);

                if (fixture != null)
                {
                    Cache.Set($"GetFixture-{id}", fixture, TimeSpan.FromSeconds((int)CacheDuration.LONG));
                }
            }

            return Mapper.Map<FixtureDto>(fixture);
        }
    }
}
