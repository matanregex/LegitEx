using Newtonsoft.Json;
using System.Collections.Generic;

namespace LegitExConsole.Dto
{
    public class RepoEventDto : BaseWebhookEventDto
    {
        [JsonProperty("events")]
        public List<string> Events { get; set; }
    }

    public class CreateRepoEventDto : RepoEventDto
    {

    }

    public class DeleteRepoEventDto : RepoEventDto
    {

    }
}
