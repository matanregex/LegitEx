using System.Collections.Generic;

namespace LegitExConsole.Dto
{
    public class RuleComposite : IRule
    {
        private readonly IList<IRule> Rules;
        public RuleComposite()
        {
            Rules = new List<IRule>();
            Rules.Add(new PushingTimeRule());
            Rules.Add(new PushingTimeRule());
        }

        public bool ValidateEvent(BaseEvent e)
        {
            foreach (var rule in Rules)
            {
                if (!rule.ValidateEvent(e))
                    return false;
            }

            return true;
        }
    }
}