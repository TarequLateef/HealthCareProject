
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.SignalR;

namespace Health.Motabea.Core.DTOs.External
{
    public class ConfirmBookHub : Hub
    {
        /*public override Task OnConnectedAsync()
        {
            var connID = Context.ConnectionId;
            var oldConnID = connID;
            return base.OnConnectedAsync();
        }*/

        public async Task Confirmation(Booking booking) =>
            await this.Clients.All.SendAsync(HubVars.ClintMethod, booking);
    }

    public struct HubVars
    {
        public const string Url = "/confirmBook";
        public const string takeBook = "book";
        public const string HubMethod = "Confirmation";
        public const string ClintMethod = "AddBook";
    }
}
