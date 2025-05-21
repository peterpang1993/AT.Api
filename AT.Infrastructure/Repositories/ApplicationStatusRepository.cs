using AT.Domain.Entities;
using AT.Domain.Interfaces;
using AT.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.Repositories
{
    public class ApplicationStatusRepository : BaseRepository<ApplicationStatus>, IApplicationStatusRepository
    {        
        public ApplicationStatusRepository(ATDbContext dbContext) : base(dbContext)
        {           
        }
    }
}
