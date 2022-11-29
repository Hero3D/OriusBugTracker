using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orius.Models
{
    public class DashboardViewModel
    {
        public string TopSubmitter { get; set; }
        public int TopSubmissions { get; set; }

        public int HighPriorityCloses { get; set; }
        public string HighPriorityCloser { get; set; }

        public int TotalReports { get; set; }
        public int OpenReports { get; set; }
        public int ClaimedReports { get; set; }
        public int ClosedReports { get; set; }
    }
}