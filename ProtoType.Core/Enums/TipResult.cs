using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Enums
{
    public enum TipResult
    {
        PENDING = 0,
        WIN = 1,
        WIN_HALF = 2,
        LOSE = 3,
        LOSE_HALF = 4,
        VOID = 5,
        PUSH = 6,
        PLACED = 7,
        UNKNOWN = 8
    }
}
