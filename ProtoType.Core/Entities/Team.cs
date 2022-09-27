using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Entities
{
    public class Team : Entity<int>
    {
        public Team()
        {
            Players = new List<Player>();
        }
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
