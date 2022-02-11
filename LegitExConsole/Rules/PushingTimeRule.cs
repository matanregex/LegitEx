using System;

namespace LegitExConsole.Dto
{
    public class PushingTimeRule : IRule
    {
        public TimeSpan MinTimespan { get; set; } = TimeSpan.FromHours(14);
        public TimeSpan MaxTimespan { get; set; } = TimeSpan.FromHours(16);

        public bool ValidateEvent(BaseEvent e)
        {
            var timeOfDay = e.EventDate.TimeOfDay;
            return timeOfDay > MinTimespan ||
                timeOfDay < MaxTimespan;
        }
    }
}