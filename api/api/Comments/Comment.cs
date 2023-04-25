using System;

namespace pillar.Comment
{
    public class Comment
    {
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User.User User { get; set; }
    }
}