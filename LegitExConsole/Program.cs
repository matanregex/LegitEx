using LegitExConsole.Dto;
using Newtonsoft.Json;
using Smee.IO.Client;
using System;

namespace LegitExConsole
{
    class Program
    {
        private static readonly Uri RepoUri = new Uri("https://smee.io/6spenarTvxoB");
        private static readonly Uri DomainUri = new Uri("https://smee.io/TNRdSsi0POLaJLa0");
        
        static void Main(string[] args)
        {
            var repoSmeeClient = new SmeeEventConsumer(RepoUri);
            var domainSmeeClient = new SmeeClient(DomainUri);

            repoSmeeClient.OnMessage += SmeeClient_OnMessage;
            domainSmeeClient.OnMessage += SmeeClient_OnMessage;

            repoSmeeClient.StartAsync().ConfigureAwait(false).GetAwaiter();
            domainSmeeClient.StartAsync().ConfigureAwait(false).GetAwaiter();
            
            while (true)
            {
                //
            }
        }

        private static void SmeeClient_OnMessage(object sender, Smee.IO.Client.Dto.SmeeEvent e)
        {
            
        }
    }
}