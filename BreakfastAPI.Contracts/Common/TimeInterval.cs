using System;

namespace BreakfastAPI.Contracts.Common
{
    public class TimeInterval
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }

        
        public TimeInterval(DateTime startDateTime, DateTime endDateTime, DateTime lastUpdateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastUpdateTime = lastUpdateTime;
        }
        public TimeInterval()
        {
            if (StartDateTime == default(DateTime))
            {
                StartDateTime = DateTime.UtcNow;
            }
            if (LastUpdateTime == default(DateTime))
            {
                LastUpdateTime = DateTime.UtcNow;
            }
            
        }
        public TimeInterval(DateTime startDateTime, DateTime endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastUpdateTime = DateTime.UtcNow;
        }
    }
}