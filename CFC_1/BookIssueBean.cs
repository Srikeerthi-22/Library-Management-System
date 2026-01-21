using System;

namespace Com.Wipro.Library.Bean
{
    public class BookIssueBean
    {
        public int IssueID { get; set; }
        public string BookID { get; set; } = "";
        public string MemberName { get; set; } = "";
        public DateTime IssueDate { get; set; }
    }
}
