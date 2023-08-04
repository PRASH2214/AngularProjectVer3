using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterConsultationStatusType
    {
        public int ConsultationStatusId { get; set; }
        public string StatusName { get; set; }
        public int? ConsultationStatusCode { get; set; }
        public int? Status { get; set; }
    }
}
