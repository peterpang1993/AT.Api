using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain.Entities;

namespace AT.Infrastructure.EntityConfigurations
{
    public class ApplicationStatusEntityConfiguration : IEntityTypeConfiguration<ApplicationStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
        {
            builder.HasKey(x => x.Id);                
                
            builder.Property(x => x.ApplicationStatusName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
