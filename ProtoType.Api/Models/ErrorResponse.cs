using ProtoType.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Api.Models
{
    public class ErrorResponse
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
