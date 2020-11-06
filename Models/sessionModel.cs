using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models
{
    public class sessionModel
    {
        public int UserId { get; set; }
        public string UserRole { get; set; }
        public string UserRocketName { get; set; }
        public string defaultUrl { get; set; }
    }
}