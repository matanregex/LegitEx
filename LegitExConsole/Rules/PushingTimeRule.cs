using LegitExConsole.Events;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Rules
{
    public class PushingTimeRule : IRule
    {
        public TimeSpan MinTimespan { get; set; } = TimeSpan.FromHours(14);
        public TimeSpan MaxTimespan { get; set; } = TimeSpan.FromHours(16);

        private string Error = "pushing code between 14:00-16:00";

        public Tuple<bool, List<string>> ValidateEvent(BaseEvent e)
        {
            var eventTimeOfDay = e.EventDate.TimeOfDay;
            var result = eventTimeOfDay > MaxTimespan || eventTimeOfDay < MinTimespan;

            return new Tuple<bool, List<string>>(result, new List<string>() { result ? "" : Error });
        }
    }
}