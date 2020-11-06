using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models
{
    public class goalModel
    {
        public int revenueTargeted { get; set; }
        public int archivedTargeted { get; set; }
        public int POTargeted { get; set; }
        public int POArchived { get; set; }
        public int LeadTargeted { get; set; }
        public int LeadArchived { get; set; }
        public int CallTargeted { get; set; }
        public int CallArchived { get; set; }

    }
}