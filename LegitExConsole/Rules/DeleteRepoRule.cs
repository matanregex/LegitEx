using Microsoft.Extensions.Caching.Memory;
using System;

namespace LegitExConsole.Dto
{
    public class DeleteRepoRule : IRule
    {
        private static readonly MemoryCache _cache;
        private static int minOffeset = 10;
        static DeleteRepoRule()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public bool ValidateEvent(BaseEvent e)
        {
            switch (e)
            {
                case RepoCreationEvent repoCreationEvent:
                    _cache.Set(repoCreationEvent.RepositoryId, repoCreationEvent.EventDate, DateTimeOffset.UtcNow.AddHours(1));//Using UtcNow, may cause FN
                    break;
                case RepoDeletionEvent repoDeletionEvent:
                    if (_cache.TryGetValue(repoDeletionEvent.RepositoryId, out DateTime creationDate))
                    {
                        return (repoDeletionEvent.EventDate - creationDate).TotalMinutes > minOffeset;
                    }
                    break;
            }

            return true;
        }
    }
}