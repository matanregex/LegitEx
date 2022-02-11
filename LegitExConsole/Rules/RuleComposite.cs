using LegitExConsole.Events;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Rules
{
    public class RuleComposite : IRule
    {
        private readonly IList<IRule> Rules;
        public RuleComposite()
        {
            Rules = new List<IRule>();
            Rules.Add(new PushingTimeRule());
            Rules.Add(new HackerTeamRule());
            Rules.Add(new DeleteRepoRule());
        }

        public Tuple<bool, List<string>> ValidateEvent(BaseEvent e)
        {
            List<string> errors = new List<string>();

            foreach (var rule in Rules)
            {
                var eventSuccess = rule.ValidateEvent(e);
                if (!eventSuccess.Item1)
                {
                    errors.AddRange(eventSuccess.Item2);
                }
            }

            return new Tuple<bool, List<string>>(errors.Count == 0, errors);
        }
    }
}