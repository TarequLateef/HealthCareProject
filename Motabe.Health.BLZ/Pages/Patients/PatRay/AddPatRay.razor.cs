using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatRay
{
    public partial class AddPatRay
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvHideForm { get; set; }
        [Parameter] public EventCallback<bool> EvExitForm { get; set; }
        [Inject] PatRayService _pRayServ { get; set; }
        [Inject] IMapper pRayMap { get; set; }
        [Parameter] public string patRayID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        bool blUpdate = false;
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(patRayID))
                _pRayServ.Item = new PatientRays();
            else
            {
                _pRayServ.Item = await _pRayServ.GetData(_pRayServ.DetailsUrl, patRayID);
                blUpdate=true;
            }
            //
            _pRayServ.ErrorList.Clear();
            _pRayServ.ErrorList.Add(new ErrorStatus { FieldID=PatRayFields.Name });
        }

        async Task SavePatRay()
        {
            //
            pRayMap.Map(_pRayServ.Item, _pRayServ.OperationItem);
            if (string.IsNullOrEmpty(patRayID))
            {
                var newpB = await _pRayServ.AddItem(_pRayServ.OperationItem, _pRayServ.AddUrl);
                if (newpB.IsSuccessStatusCode)
                    await EvHideForm.InvokeAsync(true);
            }
            else
            {
                var upPatBio =
                    await _pRayServ.UptateItem(_pRayServ.OperationItem, _pRayServ.UpdateUrl);
                if (upPatBio.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pRayServ.OperationItem.PRID);

            }
        }
    }
}
