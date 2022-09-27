using ProtoType.Core.Entities;
using ProtoType.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Interfaces.Repositories
{
    public interface IResultService
    {
        TipResult ResultCorrectScore(Fixture fixture, int homeTeamScore, int awayTeamScore);
    }
}
