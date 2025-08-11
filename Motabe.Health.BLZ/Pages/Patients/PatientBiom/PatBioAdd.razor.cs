using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBiom
{
    public partial class PatBioAdd
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public EventCallback<bool> EvHideBio { get; set; }
        [Parameter] public EventCallback<bool> EvExitForm { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public string patBioID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }

        [Inject] PatBioService _pBioServ { get; set; }
        [Inject] IMapper pBioMap { get; set; }
        bool blUpdate = false;
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(patBioID))
                _pBioServ.Item = new PatientBio();
            else
            {
                _pBioServ.Item = await _pBioServ.GetData(_pBioServ.DetailsUrl, patBioID);
                blUpdate=true;
            }
            //
            _pBioServ.ErrorList.Clear();
            _pBioServ.ErrorList.Add(new ErrorStatus { FieldID=PatBioFields.Bio });
        }

        async Task SavePatBio()
        {
            //
            pBioMap.Map(_pBioServ.Item, _pBioServ.OperationItem);
            if (string.IsNullOrEmpty(patBioID))
            {
                var newpB = await _pBioServ.AddItem(_pBioServ.OperationItem, _pBioServ.AddUrl);
                if (newpB.IsSuccessStatusCode)
                    await EvHideBio.InvokeAsync(true);
            }
            else
            {
                var upPatBio =
                    await _pBioServ.UptateItem(_pBioServ.OperationItem, _pBioServ.UpdateUrl);
                if (upPatBio.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pBioServ.OperationItem.PatBioID);
            }
        }
    }
}
