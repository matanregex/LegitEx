using System;

namespace LegitExConsole.Dto
{
    public class HackerTeamRule : IRule
    {
        public string ProhibitPrefix { get; set; } = "hacker";

        public bool ValidateEvent(BaseEvent e)
        {
            if (e is TeamCreationEvent teamCreationEvent)
            {
                return teamCreationEvent.TeamName.StartsWith(ProhibitPrefix, StringComparison.OrdinalIgnoreCase);
            }
            
            return true;
        }
    }
}