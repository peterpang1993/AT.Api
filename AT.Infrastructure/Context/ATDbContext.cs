using AT.Domain.Entities;
using AT.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.Context
{
    public class ATDbContext : DbContext
    {
        public ATDbContext(DbContextOptions<ATDbContext> options): base(options)
        {            
        }
        
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationEntityConfiguration());            
            modelBuilder.ApplyConfiguration(new ApplicationStatusEntityConfiguration());
        }
    }
}
