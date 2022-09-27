using ProtoType.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Api
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbTransaction _dbTransaction;

        public IFixtureService FixtureService { get; }

        public UnitOfWork(IDbTransaction dbTransaction, IFixtureService fixtureService)
        {
            _dbTransaction = dbTransaction;
            FixtureService = fixtureService;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
