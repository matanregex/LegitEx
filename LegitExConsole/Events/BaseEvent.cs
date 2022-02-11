using System;

namespace LegitExConsole.Dto
{
    public abstract class BaseEvent
    {
        public DateTime EventDate { get; set; }
        public string PusherName { get; set; }
    }
}