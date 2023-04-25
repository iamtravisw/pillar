using System;

namespace pillar.Ticket
{
    public class Ticket
    {
        public User.User User { get; set; }
        public int TicketId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Agent { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime FirstReplyDate { get; set; }
        public DateTime SolveDate { get; set; }
        public DateTime ReopenDate { get; set; }
        public DateTime FinalSolveDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}