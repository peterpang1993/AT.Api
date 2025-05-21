using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Domain.Entities
{
    public abstract class TransactionalBaseEntity : BaseEntity
    {
        public DateTime CreatedDateTime { get; protected set; } = DateTime.UtcNow;
        public DateTime ModifiedDateTime { get; protected set; } = DateTime.UtcNow;
    }
}
