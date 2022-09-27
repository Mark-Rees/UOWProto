using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Entities
{
    public class Fixture : Entity<int>
    {
        public string Name { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public DateTime StartTime { get; set; }
    }
}
