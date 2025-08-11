using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatMeds
{
    public partial class AddPatMed
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvHideForm { get; set; }
        [Parameter] public EventCallback<bool> EvExitForm { get; set; }
        [Inject] PatMedService _pMedServ { get; set; }
        [Inject] IMapper pMedMap { get; set; }
        [Parameter] public string patTreatID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        bool blUpdate = false;
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(patTreatID))
                _pMedServ.Item = new PatMed();
            else
            {
                _pMedServ.Item = await _pMedServ.GetData(_pMedServ.DetailsUrl, patTreatID);
                blUpdate=true;
            }
            //
            _pMedServ.ErrorList.Clear();
            _pMedServ.ErrorList.Add(new ErrorStatus { FieldID=PatMedFields.Name });
        }

        async Task SavePatBio()
        {
            //
            pMedMap.Map(_pMedServ.Item, _pMedServ.OperationItem);
            if (string.IsNullOrEmpty(patTreatID))
            {
                var newpB = await _pMedServ.AddItem(_pMedServ.OperationItem, _pMedServ.AddUrl);
                if (newpB.IsSuccessStatusCode)
                    await EvHideForm.InvokeAsync(true);
            }
            else
            {
                var upPatBio =
                    await _pMedServ.UptateItem(_pMedServ.OperationItem, _pMedServ.UpdateUrl);
                if (upPatBio.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pMedServ.OperationItem.PatMedID);
            }
        }
    }
}
