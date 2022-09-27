using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Core.Enums
{
    public enum CacheDuration
    {
        MICRO = 10,
        MINUTE = 60,
        SHORT = 300,
        MEDIUM = 1800,
        LONG = 3600,
        DAY = 86400,
        WEEK = 604800,
    }
}
