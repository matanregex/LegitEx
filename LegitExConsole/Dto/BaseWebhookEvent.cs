using Newtonsoft.Json;
using System;

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
}
