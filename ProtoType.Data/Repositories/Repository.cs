using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Data.Repositories
{
    public abstract class Repository
    {
        protected IDbTransaction Transaction { get; private set; }

        public Repository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
