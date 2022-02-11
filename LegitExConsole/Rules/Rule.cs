using LegitExConsole.Events;
using System;
using System.Collections.Generic;

namespace LegitExConsole.Rules
{
    public interface IRule
    {
        Tuple<bool, List<string>> ValidateEvent(BaseEvent e);
    }
}