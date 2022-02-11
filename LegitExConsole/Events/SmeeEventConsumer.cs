using LegitExConsole.Dto;
using LegitExConsole.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smee.IO.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegitExConsole.Events
{
    public class SmeeEventConsumer : IBaseEventConsumer
    {
        public event EventHandler<BaseEvent> OnMessage;
        JsonSerializerSettings settings = new JsonSerializerSettings();

        public SmeeEventConsumer(Uri uri)
        {
            settings.Error = (serializer, err) =>
            {
                err.ErrorContext.Handled = true;
            };
            var client = new SmeeClient(uri);
            client.OnMessage += Client_OnMessage;
            client.StartAsync().ConfigureAwait(false).GetAwaiter();
        }

        private void Client_OnMessage(object sender, Smee.IO.Client.Dto.SmeeEvent e)
        {
            if (e?.Data?.Body == null)
            {
                return;
            }

            var _object = JsonConvert.DeserializeObject<JObject>(e.Data.Body.ToString());
            var rc = new RuleComposite();
            Tuple<bool, List<string>> response = null;

            try
            {
                if (_object.ContainsKey("pusher"))
                {
                    var _event = JsonConvert.DeserializeObject<PushingCodeEventDto>(e.Data.Body.ToString(), settings);
                    var _ = new CommitEvent() { EventDate = ConvertDateFromEpoch(_event.Repository.Pushed), PusherName = _event.Pusher.Name };
                    response = rc.ValidateEvent(_);
                }
                else if (_object.ContainsKey("events") && _object["events"].Contains("repository"))
                {
                    //TODO - Find what shared between create and delete repo and how to diffrentiate
                    var _event = JsonConvert.DeserializeObject<CreateRepoEventDto>(e.Data.Body.ToString(), settings);
                    var _ = new RepoCreationEvent() { EventDate = ConvertDateFromEpoch(_event.Repository.Pushed), RepositoryId = _event.Repository.Id};
                    response = rc.ValidateEvent(_);
                }
                else if (_object.ContainsKey("team"))//Not sure how to trigger this event
                {
                    var _event = JsonConvert.DeserializeObject<CreateTeamEventDto>(e.Data.Body.ToString(), settings);
                    var _ = new TeamCreationEvent() { EventDate = ConvertDateFromEpoch(_event.Repository.Pushed), TeamName = _event.TeamName };
                    response = rc.ValidateEvent(_);
                }
                else
                {
                    Console.WriteLine("Unsupported event");
                }

                if (!response.Item1)
                {
                    Console.WriteLine($"Errors: {string.Join(",", response.Item2)} While adding event: {e.Data.Body}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing: {e?.Data?.Body}", ex);
                return;
            }
        }

        private DateTime ConvertDateFromEpoch(long epoch)
        {
            return DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
        }
    }
}