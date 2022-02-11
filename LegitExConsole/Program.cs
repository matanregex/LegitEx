using LegitExConsole.Events;
using System;

namespace LegitExConsole
{
    class Program
    {
        private static readonly Uri RepoUri = new Uri("https://smee.io/6spenarTvxoB");
        private static readonly Uri DomainUri = new Uri("https://smee.io/TNRdSsi0POLaJLa0");
        
        static void Main(string[] args)
        {
            var repoClientConsumer = EventFactory.Instance.GetConsumer(RepoUri);
            var domainClientConsumer = EventFactory.Instance.GetConsumer(DomainUri);
            
            while (true)//keep conncetion
            {
                //
            }
        }
    }
}