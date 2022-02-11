using System;

namespace LegitExConsole.Events
{
    public abstract class BaseEvent
    {
        public DateTime EventDate { get; set; }
        public string PusherName { get; set; }
    }
}