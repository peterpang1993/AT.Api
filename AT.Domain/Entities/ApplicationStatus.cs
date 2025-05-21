using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AT.Domain.Entities
{
    public class ApplicationStatus : BaseEntity
    {        
        public string ApplicationStatusName { get; private set; } = null!;

        private ApplicationStatus()
        {
            
        }
        public ApplicationStatus(int id, string applicationStatusName)
        {
            Id = id;
            ApplicationStatusName = applicationStatusName;

            if (string.IsNullOrWhiteSpace(applicationStatusName))
                throw new ArgumentException("Status name is required.", nameof(applicationStatusName));

            Id = id;
            ApplicationStatusName = applicationStatusName;
        }

        public override string ToString() => ApplicationStatusName;
    }
}
