using System;
using System.ComponentModel.DataAnnotations;

public enum TicketPriority { Low, Medium, High, Urgent}
public enum TicketStatus { Open, Claimed, Closed}

namespace Orius.Models
{
    public class TicketModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = String.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(maximumLength: 5000, MinimumLength = 3, ErrorMessage = "Must be between 3 and 5000 characters")]
        public string Description { get; set; } = String.Empty;

        public string Submitter { get; set; } = "John Doe";

        public DateTime TimeSubmitted { get; set; } = DateTime.Now;

        public TicketPriority Priority { get; set; } = TicketPriority.Low;

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public int Claimer { get; set; }
    }
}