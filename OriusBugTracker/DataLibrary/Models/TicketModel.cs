using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Submitter { get; set; }
        public DateTime DateTime { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int Claimer { get; set; }
    }
}
