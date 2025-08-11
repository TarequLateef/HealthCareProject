using AutoMapper;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatCts
{
    public partial class PatCtOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatCtService _pCtServ { get; set; }
        [Inject] CtService _ctServ { get; set; }
        [Inject] IMapper ctMap { get; set; }

        [Parameter] public PatCt PatCtItem { get; set; }
        [Parameter] public EventCallback<PatCt> EvPatCt { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatCtErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _pCtServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            PatCtItem.BookID = BookItem.BookID;
            PatCtItem.PatID=BookItem.PatientID;
            PatCtItem.UserLogID=_authServ.Item.UserLoginID;
            PatCtItem.CompID = _authServ.Item.CompID;
            //
            await this.LoadCtList();
            //
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
        }

        async Task LoadCtList()
        {
            //
            _ctServ.OperationList = await _ctServ.GetDataList(_ctServ.ListUrl);
            _ctServ.DataList =
                _ctServ.OperationList.Where(s => s.CompTypeID==_authServ.Item.CompTypeID).ToList();

        }

        #region Sypmpotons
        void SelectCt(string CtID)
        {
            PatCtItem.CTID=CtID;
            EvPatCt.InvokeAsync(PatCtItem);
            EvPatCtErr.InvokeAsync(_pCtServ.ErrorList);
        }

        async Task AddCt(string CtName)
        {
            _ctServ.Item=new CT
            {
                CT_ID=PatCtItem.CTID,
                CompTypeID=_authServ.Item.CompTypeID,
                UserLogID=_authServ.Item.UserLoginID,
                CT_Name=CtName
            };
            ctMap.Map(_ctServ.Item, _ctServ.OperationItem);
            var addSy = await _ctServ.AddItem(_ctServ.OperationItem, _ctServ.AddUrl);
            if (addSy.IsSuccessStatusCode)
            {
                var ctData = await addSy.Content.ReadAsStringAsync();
                _ctServ.Item = JsonConvert.DeserializeObject<CT>(ctData)??new CT();
                await this.LoadCtList();
                this.SelectCt(_ctServ.Item.CT_ID);
            }
        }
        #endregion

        void SelectCtResult(string lvl)
        {
            PatCtItem.CtResult= lvl;
            EvPatCt.InvokeAsync(PatCtItem);
            EvPatCtErr.InvokeAsync(_pCtServ.ErrorList);
        }

        void SetPatDate(string dt)
        {
            PatCtItem.StartDate=Convert.ToDateTime(dt);
            EvPatCt.InvokeAsync(PatCtItem);
        }

    }
}
