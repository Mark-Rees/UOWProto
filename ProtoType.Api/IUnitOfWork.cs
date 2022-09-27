using ProtoType.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Api
{
    public interface IUnitOfWork
    {
        void Commit();
        IFixtureService FixtureService { get; }
    }
}
