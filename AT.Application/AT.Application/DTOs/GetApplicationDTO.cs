using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.DTOs
{
    public class GetApplicationDTO
    {
        public int Id { get; set; }
        public string Company { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateTime DateApplied { get; set; }
        public string ApplicationStatus { get; set; } = null!;
    }
}
