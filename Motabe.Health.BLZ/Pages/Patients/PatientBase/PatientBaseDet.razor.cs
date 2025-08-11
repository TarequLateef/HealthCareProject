using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBase
{
    public partial class PatientBaseDet
    {

        [Parameter,SupplyParameterFromQuery] public string pb { get; set; }
        [Parameter,SupplyParameterFromQuery] public string? pd { get; set; }
        [Parameter] public bool WithModal { get; set; } = false;
        [Parameter] public Booking BookItem { get; set; }

        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] PatientDetailsService _pDetServ { get; set; }
        [Inject] BookingService _bServ { get; set; }
        [Inject] NavigationManager pNav { get; set; }
        [Inject] IMapper pMap { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatMedService _pMedServ { get; set; }

        string[] PatTabs =
            { "Details"};
        string[] exTabs = { "Symptoms", "Biometrics", "Analysis", "C.T.", "X-Rays", "Treatment" };
        #region Private
        bool ShowAddSymp = false;
        bool ShowAddBio = false;
        bool ShowAddAnal = false;
        bool ShowAddPat = false;
        bool ShowAddCT = false;
        bool ShowAddRay = false;
        bool ShowAddTreat = false;
        string currTab = string.Empty;
        int currIndx = 0;
        string replyDay = "0";

        string patSymID = string.Empty;
        string patBioID = string.Empty;
        string patAnalID = string.Empty;
        string patCtID = string.Empty;
        string patRayID = string.Empty;
        string patTreatID = string.Empty;
        #endregion
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            _pDetServ.CurrUserID=_authServ.Item.UserLoginID;
            _pBaseServ.CurrUserID=_authServ.Item.UserLoginID;
            _bServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _pDetServ.Item = new PatientData();
            _bServ.Item = new Booking();
            //
            _pBaseServ.MainList = await _pBaseServ.GetDataList(_pBaseServ.ListUrl);
            //
            await this.LoaadTabs();
            //
            _bServ.MainList = await _bServ.GetDataList(_bServ.ListUrl, true);
            _bServ.OperationList =
                string.IsNullOrEmpty(pd) ? _bServ.MainList :
                    _bServ.MainList.Where(b => b.StartDate.ToString("yyyyMMdd")==pd.Substring(0, 8) && b.EnsureBook).ToList();
            BookItem = _bServ.OperationList.First(b => b.PatientID==pb);
            //
            _pBaseServ.OperationList =
                _pBaseServ.MainList.Where(p => _bServ.OperationList.Any(b => b.PatientID==p.PatientID)).ToList();
            _pBaseServ.DataList = _pBaseServ.OperationList;
            _pBaseServ.Item = _pBaseServ.DataList.First(p => p.PatientID==pb);
            currIndx=_pBaseServ.DataList.IndexOf(_pBaseServ.Item);
            //

            currTab=PatTabs[0];
        }

        async Task LoaadTabs()
        {
            //
            _pDetServ.Item = await _pDetServ.GetData(_pDetServ.DetailsUrl, pb);
            if (_pDetServ.Item.PatientID is not null)
                foreach (var item in exTabs)
                {
                    IList<string> ptList = PatTabs.ToList();
                    ptList.Add(item);
                    PatTabs=ptList.ToArray();
                }
        }

        void NavPats(int indx)
        {
            var pat = _pBaseServ.OperationList[indx];
            pNav.NavigateTo(_pBaseServ.DetailsWin+PatBaseFields.ParamPID+pat.PatientID+PatBaseFields.ParamBookDate+BookItem.StartDate.ToString("yyyyMMdd"), true);
        }

        async Task AddTreat(bool add)
        {
            _pMedServ.CurrUserID=_authServ.Item.UserLoginID;
            _pMedServ.DataList =
                await _pMedServ.GetDataList(_pMedServ.SpecPatMed+_pMedServ.CurrUserID+PatMedFields.ParamDate+BookItem.StartDate+PatMedFields.ContPat+_pBaseServ.Item.PatientID);
            ShowAddTreat=!add;
        }

        async Task AddNewBook()
        {
            #region Stop Book
            pMap.Map(BookItem, _bServ.OperationItem);
            var upBook = await _bServ.StopRestoreItem(_bServ.OperationItem, _bServ.StopRestoreUrl);
            #endregion
            if (upBook.IsSuccessStatusCode)
            {
                #region Add Book
                _bServ.Item.UserLogID = _authServ.Item.UserLoginID;
                _bServ.Item.PatientID=BookItem.PatientID;
                _bServ.Item.CompID=_authServ.Item.CompID;
                _bServ.Item.Repeated=true;
                pMap.Map(_bServ.Item, _bServ.OperationItem);
                var addBook = await _bServ.AddItem(_bServ.OperationItem, _bServ.AddUrl);
                if (addBook.IsSuccessStatusCode) this.NavPats(currIndx+1);
                #endregion
            }
        }
    }
}
