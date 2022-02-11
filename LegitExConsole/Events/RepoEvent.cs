namespace LegitExConsole.Events
{
    public class RepoEvent : BaseEvent
    {
        public long RepositoryId { get; set; }
    }

    public class RepoCreationEvent : RepoEvent
    {
    }

    public class RepoDeletionEvent : RepoEvent
    {
    }
}