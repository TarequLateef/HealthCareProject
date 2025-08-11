
using AutoMapper;
using GeneralMotabea.Core.DTOs;
using Health.Motabea.Core.DTOs.External;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.Bookings
{
    public partial class RegistBook
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] BookingService _bServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] NavigationManager _bookNav { get; set; }
        [Inject] IMapper _bookMap { get; set; }

        [Parameter] public DateTime? BookDate { get; set; }
        int registCount = 0;
        bool docAssiss => _authServ.Item.Roles.Any(r => r==RolesList.Doc_Ass);
        HubConnection _conn;
        bool IsConnected => _conn.State==HubConnectionState.Connected;
        Booking bItem = new Booking();
        bool itemCons { get; set; } = false;
        string consTitle { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            //
            await _authServ.ReadLogData();
            _bServ.CurrUserID=_authServ.Item.UserLoginID;
            _pBaseServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _conn = new HubConnectionBuilder()
            .WithUrl(_bookNav.ToAbsoluteUri(HubVars.Url))
            .Build();
            await _conn.StartAsync();
            //
            DateTime dt = BookDate.HasValue ? BookDate.Value : DateTime.Now;
            _pBaseServ.Item = new PatientBaseData();
            _bServ.Item = new Booking()
            {
                CompID=_authServ.Item.CompID,
                UserLogID=_authServ.Item.UserLoginID
            };
            _bServ.OperationItem = new BookDTO();
            //
            _bServ.MainList = await _bServ.GetDataList(_bServ.ListUrl+BookFields.DateParam+
                dt, true);
            _pBaseServ.MainList =await _pBaseServ.GetDataList(_pBaseServ.ListUrl);
            //
            this.ReloadBook(dt);
            //
            #region Errors
            _bServ.ErrorList.Clear();
            _bServ.ErrorList.Add(new ErrorStatus { FieldID=BookFields.Patient });
            /*_bServ.ErrorList.Add(new ErrorStatus { FieldID=BookFields.Date });*/
            #endregion
        }

        void ReloadBook(DateTime date)
        {
            _bServ.OperationList =
                _bServ.MainList.Where(b => b.StartDate.Date==date.Date)
                .OrderBy(b => b.Ordering).ToList();
            registCount=_bServ.OperationList.Count;
            _bServ.ChangePage(1);
        }

        void GetBooking(Booking book)
        {
            _bServ.Item = book;
            this.ReloadBook(book.StartDate);
        }

        async Task BookingP()
        {
            #region Confirm
            _bServ.Item.BookID=string.Empty;
            _bServ.Item.EnsureBook=true;
            _bServ.Item.Ordering=_bServ.OperationList.Count(b => b.EnsureBook)+1;
            #endregion
            _bookMap.Map(_bServ.Item, _bServ.OperationItem);
            var book = await _bServ.AddItem(_bServ.OperationItem, _bServ.AddUrl);
            if (book.IsSuccessStatusCode)
            {/*_bookNav.NavigateTo(_bServ.AddWin, true);*/
                var bData = await book.Content.ReadAsStringAsync();
                _bServ.Item = JsonConvert.DeserializeObject<Booking>(bData)??new Booking();
                _bServ.MainList.Add(_bServ.Item);
                this.ReloadBook(_bServ.Item.StartDate.Date);
            }
        }

        async Task Confirmation(string itemID)
        {
            bItem = _bServ.OperationList.First(b => b.BookID==itemID);
            bItem.EnsureBook=!bItem.EnsureBook;
            if (bItem.EnsureBook)
            {
                bItem.Ordering=_bServ.OperationList.Count(b => b.EnsureBook==bItem.EnsureBook);
                int bIndx = _bServ.OperationList.IndexOf(bItem);
                _bookMap.Map(bItem, _bServ.OperationItem);
                var confBook = await _bServ.UptateItem(_bServ.OperationItem, _bServ.UpdateUrl);
                if (confBook.IsSuccessStatusCode)
                {
                    _bServ.OperationList[bIndx]=bItem;
                    if (IsConnected)
                    {
                        await _conn.SendAsync(HubVars.HubMethod, bItem);
                        await _conn.StopAsync();
                    }
                }
            }
        }

        public void Dispose() => _=_conn.DisposeAsync();
    }
}
