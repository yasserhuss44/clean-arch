 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Persistence.Db
{
    public class SchoolUnitOfWork<TContext> : UnitOfWork<TContext>, ISchoolUnitOfWork<TContext> where TContext : SchoolDbContext
    {
        public SchoolUnitOfWork(TContext context) : base(context)
        {

        }
    }
}
