using System;

namespace LegitExConsole.Dto
{
    public interface IRule
    {
        bool ValidateEvent(BaseEvent e);
    }
}