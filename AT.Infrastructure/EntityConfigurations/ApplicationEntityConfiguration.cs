using AT.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.EntityConfigurations
{
    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            new TransactionalBaseEntityConfiguration<Application>().Configure(builder);

            builder.Property(x => x.Company)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DateApplied)
                .IsRequired();

            builder.HasOne(x => x.ApplicationStatus)
                .WithMany()
                .HasForeignKey(x => x.ApplicationStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
