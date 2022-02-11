using System;
using System.Collections.Generic;
using System.Text;

namespace LegitExConsole.Events
{
    public class EventFactory
    {
        private static readonly Lazy<EventFactory> lazy = new Lazy<EventFactory>(() => new EventFactory());

        public static EventFactory Instance { get { return lazy.Value; } }

        public IBaseEventConsumer GetConsumer(Uri uri)
        {
            //Can serve other implementations
            return new SmeeEventConsumer(uri);
        }
    }
}
