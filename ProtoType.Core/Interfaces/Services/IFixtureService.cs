using ProtoType.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Interfaces.Services
{
    public interface IFixtureService
    {
        Task<FixtureDto> Get(int id);
    }
}
