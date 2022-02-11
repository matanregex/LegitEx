using Smee.IO.Client;
using System;

namespace LegitExConsole.Dto
{
    public interface IBaseEventConsumer
    {
        public event EventHandler<BaseEvent> OnMessage;
    }

    public class SmeeEventConsumer : IBaseEventConsumer
    {
        public event EventHandler<BaseEvent> OnMessage;

        public SmeeEventConsumer(Uri uri)
        {
            var client = new SmeeClient(uri);
            client.OnMessage += Client_OnMessage;
            client.StartAsync().ConfigureAwait(false).GetAwaiter();
        }

        private void Client_OnMessage(object sender, Smee.IO.Client.Dto.SmeeEvent e)
        {
            //Convert to base event
            //var _event = //baseEvent
            OnMessage(sender, null /*_event*/);
        }
    }
}