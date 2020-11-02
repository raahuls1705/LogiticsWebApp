using System;
using System.Collections.Generic;

namespace LogiticsWebApp.Entities
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public int? LogLevelId { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public string LoggedUserId { get; set; }
        public string PageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
