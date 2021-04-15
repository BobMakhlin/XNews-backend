using System;

namespace Domain.Logging.Entities
{
    public class ApplicationLog
    {
        public int ApplicationLogId { get; set; }
        public string MachineName { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
    }
}
