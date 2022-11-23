using System.Collections.Generic;

namespace Orius.Scripts
{
    public static class ElementColorLibrary
    {
        public static string GetPriorityColor(TicketPriority priority)
        {
            var lib = new Dictionary<TicketPriority, string>();

            lib.Add(TicketPriority.Low, "#001BFF");
            lib.Add(TicketPriority.Medium, "#0A8C61");
            lib.Add(TicketPriority.High, "#BD1111");
            lib.Add(TicketPriority.Urgent, "#4D0A7B");

            return lib[priority];
        }

        public static string GetStatusColor(TicketStatus status)
        {
            var lib = new Dictionary<TicketStatus, string>();

            lib.Add(TicketStatus.Closed, "#AEB6BF");
            lib.Add(TicketStatus.Claimed, "#F1C40F");
            lib.Add(TicketStatus.Open, "#2ECC71");

            return lib[status];
        }
    }
}