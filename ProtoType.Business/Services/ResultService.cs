using ProtoType.Core.Entities;
using ProtoType.Core.Enums;
using ProtoType.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Business.Services
{
    public class ResultService : IResultService
    {
        public TipResult ResultCorrectScore(Fixture fixture, int homeTeamScore, int awayTeamScore)
        {
            return homeTeamScore == fixture.HomeTeamScore && awayTeamScore == fixture.AwayTeamScore ? TipResult.WIN : TipResult.LOSE;
        }
    }
}
