using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatMeds
{
    public partial class PatMedOperList
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] MedicineService _medServ { get; set; }
        [Inject] PatMedService _pMedServ { get; set; }
        [Inject] IMapper pMedMap { get; set; }
        [Parameter] public Booking BookingItem { get; set; }
        [Parameter] public EventCallback<IList<PatMed>> EvMedPatList { get; set; }

        PatMed patMedItem = new PatMed();
        string medAction = string.Empty;
        string chkMedID = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _authServ.Item = await _authServ.ReadLogData();
            //
            _pMedServ.CurrUserID=_authServ.Item.UserLoginID;
            _pMedServ.Item = new PatMed();
            //
            _medServ.OperationList = await _medServ.GetDataList(_medServ.ListUrl, true);
            _medServ.ChangePage(1);
            //
            _pMedServ.OperationList =
                await _pMedServ.GetDataList(_pMedServ.SpecPatMed+_pMedServ.CurrUserID+PatMedFields.ContPat+BookingItem.PatientID+PatMedFields.ParamDate+BookingItem.StartDate.Date, true);
            _pMedServ.DataList=_pMedServ.OperationList;
            //
            medAction = "Take";
        }

        async Task SaveMed()
        {
            _pMedServ.Item.PatID=BookingItem.PatientID;
            _pMedServ.Item.BookID=BookingItem.BookID;
            _pMedServ.Item.CompID=BookingItem.CompID;
            _pMedServ.Item.UserLogID=_authServ.Item.UserLoginID;

            if (string.IsNullOrEmpty(_pMedServ.Item.Dose))
                _pMedServ.Item.Dose="1";
            if (string.IsNullOrEmpty(_pMedServ.Item.Period))
                _pMedServ.Item.Period="Day";
            pMedMap.Map(_pMedServ.Item, _pMedServ.OperationItem);
            var regMed = await _pMedServ.AddItem(_pMedServ.OperationItem, _pMedServ.AddUrl);
            if (regMed.IsSuccessStatusCode)
            {
                var regData = await regMed.Content.ReadAsStringAsync();
                var pMedItem = JsonConvert.DeserializeObject<PatMed>(regData)??new PatMed();
                if (_pMedServ.OperationList.Any(pm => pm.MedID==pMedItem.MedID))
                {
                    #region Replace PatMed
                    PatMed lstItem = _pMedServ.OperationList.First(pm => pm.MedID==pMedItem.MedID);
                    int lstIndx = _pMedServ.OperationList.IndexOf(lstItem);
                    _pMedServ.OperationList[lstIndx]=lstItem;
                    #endregion
                    #region Replace Med
                    /*Medicine medItem = _medServ.OperationList.First(m => m.MedID==pMedItem.MedID);
                    int mdIndx = _medServ.OperationList.IndexOf(medItem);
                    _medServ.OperationList[mdIndx]=medItem;*/
                    #endregion
                    await EvMedPatList.InvokeAsync(_pMedServ.OperationList);
                }
            }
        }
    }
}
