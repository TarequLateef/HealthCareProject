using GeneralMotabea.Core.DTOs;
using Health.Motabea.Core.DTOs.External;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Motabe.Health.BLZ.Pages.Patients.Bookings
{
    public partial class BookList : ComponentBase
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] BookingService _bServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] NavigationManager bNav { get; set; }

        DateTime chkDate = DateTime.Now;
        string infoID = string.Empty;
        HubConnection bookHubConn;
        bool IsConnected => 
            bookHubConn is not null &&  bookHubConn.State==HubConnectionState.Connected;
        bool docAssiss => _authServ.Item.Roles.Any(r => r==RolesList.Doc_Ass);
        protected override async Task OnInitializedAsync()
        {
            //
            await _authServ.ReadLogData();
            _bServ.CurrUserID=_authServ.Item.UserLoginID;
            await this.ReloadTable(chkDate);
            //
            bookHubConn = new HubConnectionBuilder().WithUrl(bNav.ToAbsoluteUri(HubVars.Url)).Build();
            bookHubConn.On<Booking>(HubVars.ClintMethod, (book) =>
            {
                LoadList(book.StartDate);
                StateHasChanged();
            }); 
            
            await bookHubConn.StartAsync();
            /*await LoadList(chkDate);*/
        }

        void LoadList(DateTime date) =>
            Task.Run(async () => ReloadTable(date));

        async Task ChangeDate(string dt) =>
            await ReloadTable(Convert.ToDateTime(dt));

        async Task ReloadTable(DateTime date)
        {
            _bServ.OperationList = await _bServ.GetDataList(_bServ.ListUrl + BookFields.DateParam+date, true);
            _bServ.OperationList = _bServ.OperationList.OrderBy(b => b.Ordering).ToList();
            _bServ.ChangePage(1);
            StateHasChanged();
        }

        void Dispose() => _=bookHubConn.DisposeAsync();
    }
}
