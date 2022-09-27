using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Business.Services
{
    public abstract class Service
    {
        protected readonly IMemoryCache Cache;
        protected readonly IMapper Mapper;

        public Service(IMemoryCache cache, IMapper mapper)
        {
            Cache = cache;
            Mapper = mapper;
        }
    }
}
