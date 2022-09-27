using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Dto
{
    public class FixtureDto
    {
        public string Name { get; set; }
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public DateTime StartTime { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}
