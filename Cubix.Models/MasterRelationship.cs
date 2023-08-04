using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Models
{
    public class MasterRelationship
    {
        public long RelationTypeId { get; set; }
        public int RelationCode { get; set; }
        public string RelationName { get; set; }
        public int? Status { get; set; }
    }
}
