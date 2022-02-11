using LegitExConsole.Dto;
using LegitExConsole.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smee.IO.Client;
using Smee.IO.Client.Dto;
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

        private void Client_OnMessage(object sender, SmeeEvent e)
        {
            if (e?.Data?.Body == null)
            {
                return;
            }

            var _object = JsonConvert.DeserializeObject<JObject>(e.Data.Body.ToString());
            var rc = new RuleComposite();
            var response = EvaluateEvent(e, _object, rc);

            if (!response.Item1)
            {
                Console.WriteLine($"Errors: {string.Join(",", response.Item2)} While consuming event: {e.Data.Body}");
            }
        }

        private Tuple<bool, List<string>> EvaluateEvent(SmeeEvent e, JObject _object, RuleComposite rc)
        {
            var errorMessage = "";
            try
            {
                switch (GetEventType(_object))
                {
                    case EventType.Commit:
                        var pcEvent = JsonConvert.DeserializeObject<PushingCodeEventDto>(e.Data.Body.ToString(), settings);
                        var _pcEvent = new CommitEvent() { EventDate = ConvertDateFromEpoch(pcEvent.Repository.Pushed), PusherName = pcEvent.Pusher.Name };
                        return rc.ValidateEvent(_pcEvent);
                    case EventType.RepoCreate:
                        var crEvent = JsonConvert.DeserializeObject<CreateRepoEventDto>(e.Data.Body.ToString(), settings);
                        var _crEvent = new RepoCreationEvent() { EventDate = ConvertDateFromEpoch(crEvent.Repository.Pushed), RepositoryId = crEvent.Repository.Id };
                        return rc.ValidateEvent(_crEvent);
                    case EventType.RepoDelete:
                        var drEvent = JsonConvert.DeserializeObject<CreateRepoEventDto>(e.Data.Body.ToString(), settings);
                        var _drEvent = new RepoDeletionEvent() { EventDate = ConvertDateFromEpoch(drEvent.Repository.Pushed), RepositoryId = drEvent.Repository.Id };
                        return rc.ValidateEvent(_drEvent);
                    case EventType.TeamCreation:
                        var ctEvent = JsonConvert.DeserializeObject<CreateTeamEventDto>(e.Data.Body.ToString(), settings);
                        var _ctEvent = new TeamCreationEvent() { EventDate = ConvertDateFromEpoch(ctEvent.Repository.Pushed), TeamName = ctEvent.Team?.Name };
                        return rc.ValidateEvent(_ctEvent);
                    default:
                        errorMessage = $"Unsupported event: {e.Data.Body}";
                        break;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error parsing: {e?.Data?.Body}";
                Console.WriteLine(errorMessage, ex);
            }

            return new Tuple<bool, List<string>>(false, new List<string>() { errorMessage });
        }

        private DateTime ConvertDateFromEpoch(long epoch)
        {
            return DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
        }

        private EventType? GetEventType(JObject jObject)
        {
            try
            {
                if (jObject.ContainsKey("pusher"))
                {
                    return EventType.Commit;
                }
                else if (jObject.ContainsKey("installation") &&  jObject.ContainsKey("action"))//need a better way for this uc
                {
                    if (new List<string>() { "added", "created" }.Contains(jObject["action"].ToString()))
                    {
                        return EventType.RepoCreate;
                    }
                    else if (new List<string>() { "removed", "deleted" }.Contains(jObject["action"].ToString()))
                    {
                        return EventType.RepoDelete;
                    }
                }
                else if (jObject.ContainsKey("team"))//Not sure how to trigger this event
                {
                    return EventType.TeamCreation;
                }

                Console.WriteLine($"Type not found: {jObject}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing: {jObject}", ex);
                return null;
            }
        }
    }

    public enum EventType
    {
        Commit, RepoCreate, RepoDelete, TeamCreation
    }
}