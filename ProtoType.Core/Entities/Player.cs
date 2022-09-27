using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Entities
{
    public class Player : Entity<int>
    {
        public string Name { get; set; }
        public Team Team { get; set; }
    }
}
