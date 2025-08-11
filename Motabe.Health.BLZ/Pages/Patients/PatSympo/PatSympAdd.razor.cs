using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatSympo
{
    public partial class PatSympAdd
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public string patSymID { get; set; } = string.Empty;
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvHideForm { get; set; }
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        [Inject] PatientSympService _pSympServ { get; set; }
        [Inject] IMapper pSympMap { get; set; }
        bool blUpdate = false;
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(patSymID))
                _pSympServ.Item = new PatientSymptom();
            else
            {
                _pSympServ.Item = await _pSympServ.GetData(_pSympServ.DetailsUrl, patSymID);
                blUpdate=true;
            }

            #region Errors
            _pSympServ.ErrorList.Clear();
            _pSympServ.ErrorList.Add(new ErrorStatus { FieldID=PatSysmpFields.Sympo });
            #endregion
        }

        async Task SavePatSympo()
        {
            pSympMap.Map(_pSympServ.Item, _pSympServ.OperationItem);
            if (string.IsNullOrEmpty(patSymID))
            {
                var addPatSym = await _pSympServ.AddItem(_pSympServ.OperationItem, _pSympServ.AddUrl);
                if (addPatSym.IsSuccessStatusCode) await EvHideForm.InvokeAsync(true);
            }
            else
            {
                var upPatySYM =
                    await _pSympServ.UptateItem(_pSympServ.OperationItem, _pSympServ.UpdateUrl);
                if (upPatySYM.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pSympServ.OperationItem.PatSympID);
            }
        }
    }
}
