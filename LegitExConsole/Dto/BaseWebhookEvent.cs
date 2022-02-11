using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Dto
{
    public class BaseWebhookEventDto
    {
        [JsonProperty("repository")]
        public Repository Repository { get; set; }
    }

    public class Repository
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("created_at")]
        public long Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }

        [JsonProperty("pushed_at")]
        public long Pushed { get; set; }
    }

    public class Pusher
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class PushingCodeEventDto: BaseWebhookEventDto
    {
        [JsonProperty("pusher")]
        public Pusher Pusher { get; set; }
    }

    public class CreateTeamEventDto : BaseWebhookEventDto
    {
        //Locate in response and model
        public string TeamName { get; set; }
    }

    public class RepoEventDto : BaseWebhookEventDto
    {
        [JsonProperty("events")]
        public List<string> Events { get; set; }
    }

    public class CreateRepoEventDto: RepoEventDto
    {
        
    }

    public class DeleteRepoEventDto : RepoEventDto
    {

    }
}
