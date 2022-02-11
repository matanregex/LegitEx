using LegitExConsole.Events;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Rules
{
    public class HackerTeamRule : IRule
    {
        public string ProhibitPrefix { get; set; } = "hacker";
        private string Error = "Create a team with the prefix “hacker”";

        public Tuple<bool, List<string>> ValidateEvent(BaseEvent e)
        {
            if (e is TeamCreationEvent teamCreationEvent)
            {
                var result = teamCreationEvent.TeamName.StartsWith(ProhibitPrefix, StringComparison.OrdinalIgnoreCase);
                if (!result)
                {
                    return new Tuple<bool, List<string>>(result, new List<string> { Error });
                }
            }

            return new Tuple<bool, List<string>>(true, new List<string>());
        }
    }
}