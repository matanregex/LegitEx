using Newtonsoft.Json;

namespace LegitExConsole.Dto
{
    public class PushingCodeEventDto: BaseWebhookEventDto
    {
        [JsonProperty("pusher")]
        public Pusher Pusher { get; set; }
    }
}
