using System;

namespace LegitExConsole.Events
{
    public interface IBaseEventConsumer
    {
        public event EventHandler<BaseEvent> OnMessage;
    }
}