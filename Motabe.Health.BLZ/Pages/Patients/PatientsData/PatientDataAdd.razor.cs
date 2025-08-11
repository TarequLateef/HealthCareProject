using AutoMapper;
using GeneralMotabea.Core.General;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using System.Diagnostics.Eventing.Reader;

namespace Motabe.Health.BLZ.Pages.Patients.PatientsData
{
    public partial class PatientDataAdd
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatientDetailsService _pDetServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }

        [Inject] IMapper _pDetMap { get; set; }
        [Inject] NavigationManager _pNav { get; set; }

        [Parameter] public bool ShowAsModal { get; set; } = false;
        [Parameter] public string pBaseID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvNewPatID { get; set; }
        [Parameter] public EventCallback<bool> EvHide { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }

        bool addData = false;
        protected override async Task OnInitializedAsync()
        {
            //
            /*_pBaseServ.Item = new PatientBaseData();*/
            //
            _pDetServ.Item = new PatientData();
            _pDetServ.MainList = await _pDetServ.GetDataList(_pDetServ.ListUrl);
            addData=!_pDetServ.MainList.Any(pd => pd.PatientID== pBaseID);
            if (!string.IsNullOrEmpty(pBaseID))
            {
                _pDetServ.Item.PatientID=pBaseID;
                _pBaseServ.Item = await _pBaseServ.GetData(_pBaseServ.DetailsUrl, pBaseID);
                if(!addData)
                    _pDetServ.Item = await _pDetServ.GetData(_pDetServ.DetailsUrl,pBaseID);
            }
            _pDetServ.Item.UserLogID = _authServ.Item.UserLoginID;
        }


        async Task SavePat()
        {
            _pDetServ.Item.UserLogID = _authServ.Item.UserLoginID;
            _pDetServ.Item.PatientID=_pBaseServ.Item.PatientID;
            _pDetMap.Map(_pDetServ.Item, _pDetServ.OperationItem);
            if (addData)
            {
                var addPdet = await _pDetServ.AddItem(_pDetServ.OperationItem, _pDetServ.AddUrl);
                if (addPdet.IsSuccessStatusCode) await afterSave();
            }
            else
            {
                var upPdet = await _pDetServ.UptateItem(_pDetServ.OperationItem, _pDetServ.UpdateUrl);
                if (upPdet.IsSuccessStatusCode) await afterSave();
            }
        }

        async Task afterSave()
        {
            if (ShowAsModal)
                await EvNewPatID.InvokeAsync(_pDetServ.Item.PatientID);
            else
                _pNav.NavigateTo(_pBaseServ.DetailsWin+PatBaseFields.ParamPID+_pDetServ.Item.PatientID+PatBaseFields.ParamBookDate+PatBookItem.StartDate.ToString("yyyyMMdd"), true);
            await EvHide.InvokeAsync(true);
        }
    }
}
