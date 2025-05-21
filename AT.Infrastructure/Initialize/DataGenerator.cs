using AT.Domain.Entities;
using AT.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.Initialize
{
    public class DataGenerator
    {
        public async static Task InitializeCodeTableData(IServiceProvider serviceProvider)
        {
            using (var context = new ATDbContext(serviceProvider.GetRequiredService<DbContextOptions<ATDbContext>>()))
            {
                await context.ApplicationStatuses.AddRangeAsync(
                
                    new ApplicationStatus(1, "Interview"),
                    new ApplicationStatus(2, "Offer"),
                    new ApplicationStatus(3, "Rejected")
                );
                await context.SaveChangesAsync();
            };
        }
    }
}
