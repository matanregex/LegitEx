using LegitExConsole.Events;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Rules
{
    public class DeleteRepoRule : IRule
    {
        private static readonly MemoryCache _cache;
        private static int minOffeset = 10;
        static DeleteRepoRule()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public Tuple<bool, List<string>> ValidateEvent(BaseEvent e)
        {
            var error = "Failed validation: creating a repository and deleting it in less than 10 minutes";
            switch (e)
            {
                case RepoCreationEvent repoCreationEvent:
                    _cache.Set(repoCreationEvent.RepositoryId, repoCreationEvent.EventDate, DateTimeOffset.UtcNow.AddHours(1));//Using UtcNow, may cause FN
                    break;
                case RepoDeletionEvent repoDeletionEvent:
                    if (_cache.TryGetValue(repoDeletionEvent.RepositoryId, out DateTime creationDate))
                    {
                        var response = (repoDeletionEvent.EventDate - creationDate).TotalMinutes > minOffeset;
                        if (!response)
                        {
                            return new Tuple<bool, List<string>>(false, new List<string> { error });
                        }
                    }
                    break;
            }

            return new Tuple<bool, List<string>>(true, new List<string>());
        }
    }
}