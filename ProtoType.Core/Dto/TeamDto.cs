using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Dto
{
    public class TeamDto
    {
        public string Name { get; set; }
        public IEnumerable<string> Players { get; set; }
    }
}
