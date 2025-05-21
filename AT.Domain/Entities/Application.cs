using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Domain.Entities
{
    public class Application : TransactionalBaseEntity
    {                       
        public string Company { get; private set; } = null!;
        public string Position { get; private set; } = null!;
        public DateTime DateApplied { get; private set; }
        public int ApplicationStatusId { get; private set; } // FK        
        public ApplicationStatus ApplicationStatus { get; private set; }

        private Application()
        {
            
        }

        private Application(string company, string position, DateTime dateApplied, ApplicationStatus applicationStatus )
        {
            if (string.IsNullOrEmpty(company))
                throw new ArgumentException("Company is required.");
            if (string.IsNullOrEmpty(position))
                throw new ArgumentException("Position is required.");

            Company = company;
            Position = position;
            DateApplied = dateApplied;
            ApplicationStatus = applicationStatus ?? throw new ArgumentException("Position is required.");
            ApplicationStatusId = applicationStatus.Id;
            
            CreatedDateTime = DateTime.UtcNow;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public static Application Create(string company, string position, DateTime dateApplied, ApplicationStatus status)
        {
            return new Application(company, position, dateApplied, status);
        }

        public void UpdateApplicationStatus(ApplicationStatus newApplicationStatus)
        {
            if (newApplicationStatus == null)
                throw new ArgumentNullException(nameof(newApplicationStatus));

            if (newApplicationStatus.Id == ApplicationStatus.Id)
                return;

            ApplicationStatus = newApplicationStatus;
            ApplicationStatusId = newApplicationStatus.Id;
            ModifiedDateTime = DateTime.UtcNow;
        }
    }
}
