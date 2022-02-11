namespace LegitExConsole.Dto
{
    public class CreateTeamEventDto : BaseWebhookEventDto
    {
        //Locate in response and model
        public string TeamName { get; set; }
    }
}
