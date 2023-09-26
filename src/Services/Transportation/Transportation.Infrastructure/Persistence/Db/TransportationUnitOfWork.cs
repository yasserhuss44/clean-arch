using Transportation.Infrastructure.Persistence.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.Infrastructure.Persistence.Db
{
    public class TransportationUnitOfWork<TContext> : UnitOfWork<TContext>, ITransportationUnitOfWork<TContext> where TContext : TransportationDbContext
    {
        public TransportationUnitOfWork(TContext context) : base(context)
        {

        }
    }
}
