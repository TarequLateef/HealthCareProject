
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.Bookings
{
    public partial class BookOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] BookingService _bServ { get; set; }
        [Inject] PatientDetailsService _pDetServ { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        [Parameter] public EventCallback<Booking> EvBooking { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvBookErrList { get; set; }

        bool showAddPat = false;
        bool showUpdPat = false;
        /*string PatInfoLink = string.Empty;*/
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            _bServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            BookItem.UserLogID = _authServ.Item.UserLoginID;
            BookItem.CompID = _authServ.Item.CompID;
            //
            _bServ.MainList = await _bServ.GetDataList(_bServ.ListUrl, true);
            _pBaseServ.Item = new PatientBaseData();
            _pBaseServ.MainList = await _pBaseServ.GetDataList(_pBaseServ.ListUrl);
            this.ReloadPat();
        }

        void SelectPatient(string pid)
        {
            BookItem.PatientID=pid;
            /*PatInfoLink =_bServ.Item.PatientID!="0" ?
                 _pBaseServ.DetailsWin+"/"+pid : string.Empty;*/
            _pBaseServ.Item = _pBaseServ.MainList.First(p => p.PatientID==pid);
            EvBookErrList.InvokeAsync(_bServ.ErrorList);
            EvBooking.InvokeAsync(BookItem);
        }

        async Task SetBookDate(string dt)
        {
            BookItem.StartDate = Convert.ToDateTime(dt);
            this.ReloadPat();
            await EvBooking.InvokeAsync(BookItem);
        }
        void PatToList(PatientBaseData pD)
        {
            _pBaseServ.MainList.Add(pD);
            _bServ.ErrorList.First(el => el.FieldID==BookFields.Patient).Done=true;
            SelectPatient(pD.PatientID);
            this.ReloadPat();
            showAddPat=false;
        }

        void UpdatePatList(PatientBaseData pD)
        {
            _pBaseServ.ReplaceUpdateItem(pD, _pBaseServ.MainList);
            SelectPatient(pD.PatientID);
            ReloadPat();
            showUpdPat=false;
        }

        void ReloadPat()
        {
            _bServ.OperationList = 
                _bServ.MainList.Where(b => b.StartDate.Date==BookItem.StartDate.Date).ToList();
            //
            _pBaseServ.OperationList =
                _pBaseServ.MainList.Where(p => !_bServ.OperationList.Any(b => b.PatientID==p.PatientID)).ToList();
            _pBaseServ.DataList = _pBaseServ.OperationList;
            //
            /*_pDetServ.OperationList = await _pDetServ.GetDataList(_pDetServ.ListUrl);
            _pDetServ.DataList = _pDetServ.OperationList;*/
        }

        void SearchPatient(string pat) =>
            _pBaseServ.DataList =
                _pBaseServ.OperationList.Where(pb => pb.PatientName.Contains(pat) || pb.Phone.Contains(pat)).ToList();

        void UpdatePatDate(string patID)
        {
            _pBaseServ.Item = _pBaseServ.MainList.First(p => p.PatientID==patID);
            showUpdPat=true;
        }
    }
}
