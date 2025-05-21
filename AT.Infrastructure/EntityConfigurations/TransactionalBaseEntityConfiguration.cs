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
    public class TransactionalBaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : TransactionalBaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            new BaseEntityConfiguration<T>().Configure(builder);

            builder.Property(x => x.CreatedDateTime)
                .IsRequired();

            builder.Property(x => x.ModifiedDateTime)
                .IsRequired();
        }
    }
}
