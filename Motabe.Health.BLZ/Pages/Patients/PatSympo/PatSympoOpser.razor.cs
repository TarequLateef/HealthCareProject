using AutoMapper;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatSympo
{
    public partial class PatSympoOpser
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatientSympService _pSympServ { get; set; }
        [Inject] SypoService _sympoServ { get; set; }
        [Inject] IMapper sympMap { get; set; }
 
        [Parameter] public PatientSymptom PatSympItem { get; set; }
        [Parameter] public EventCallback<PatientSymptom> EvPatSymp { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatSympErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        protected override async Task OnInitializedAsync()
        {
//
            PatSympItem.BookID = BookItem.BookID;
            PatSympItem.PatID=BookItem.PatientID;
            PatSympItem.UserLogID=_authServ.Item.UserLoginID;
            PatSympItem.CompID = _authServ.Item.CompID;
            //
            _sympoServ.OperationList = await _sympoServ.GetDataList(_sympoServ.ListUrl);
            _sympoServ.DataList =
                _sympoServ.OperationList.Where(s => s.CompTypeID==_authServ.Item.CompTypeID).ToList();
            //
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
            this.SelectLevel(SympLevel.Low);
        }

        #region Sypmpotons
        void SelectSympo(string sympID)
        {
            PatSympItem.SympID=sympID;
            EvPatSymp.InvokeAsync(PatSympItem);
            EvPatSympErr.InvokeAsync(_pSympServ.ErrorList);
        }

        async Task AddSypo(string sympName)
        {
            _sympoServ.Item=new Symptoms
            {
                SymptomID=PatSympItem.SympID,
                CompTypeID=_authServ.Item.CompTypeID,
                UserLogID=_authServ.Item.UserLoginID,
                SymptonName=sympName,

            };
            sympMap.Map(_sympoServ.Item, _sympoServ.OperationItem);
            var addSy = await _sympoServ.AddItem(_sympoServ.OperationItem, _sympoServ.AddUrl);
            if (addSy.IsSuccessStatusCode)
            {
                _sympoServ.Item.SymptomID=_sympoServ.OperationItem.SymptomID;
                _sympoServ.DataList.Add(_sympoServ.Item);
                this.SelectSympo(_sympoServ.Item.SymptomID);
            }
        }
        #endregion

        void SelectLevel(string lvl)
        {
            PatSympItem.SympStatus = lvl;
            EvPatSymp.InvokeAsync(PatSympItem);
            EvPatSympErr.InvokeAsync(_pSympServ.ErrorList);
        }

        void SetPatDate(string dt)
        {
            PatSympItem.StartDate=Convert.ToDateTime(dt);
            EvPatSymp.InvokeAsync(PatSympItem);
        }

        void SetNots(string note)
        {
            PatSympItem.Description=note;
            EvPatSymp.InvokeAsync(PatSympItem);
        }
    }
}
