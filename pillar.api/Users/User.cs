using System;
using System.ComponentModel.DataAnnotations;

namespace pillar.User
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null;
        public string Password { get; set; } = null;
        public string Organization { get; set; } = null;
        public string Requester { get; set; } = null;
        public string Title { get; set; } = null;
        public Boolean PrimaryContact { get; set; } = true;
        public Boolean Admin { get; set; } = false;
        public DateTime AddDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string UserNameLowerCase()
        {
            var lowerCaseUserName = UserName.ToLower();
            return lowerCaseUserName;
        }
    }
}