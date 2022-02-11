using Newtonsoft.Json;

namespace LegitExConsole.Dto
{
    public class CreateTeamEventDto : BaseWebhookEventDto
    {
        //https://docs.github.com/en/developers/webhooks-and-events/webhooks/webhook-events-and-payloads#team_add
        [JsonProperty("team")]
        public Team Team { get; set; }
    }
    public class Team
    {
        [JsonProperty("name")]
        public string Name{ get; set; }
    }
}
