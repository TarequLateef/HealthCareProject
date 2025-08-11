using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatAnal
{
    public partial class NewPatAnal
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvHideForm { get; set; }
        [Parameter] public EventCallback<bool> EvExitForm { get; set; }
        [Inject] PatAnalService _pAnalServ { get; set; }
        [Inject] IMapper pAnalMap { get; set; }
        [Parameter] public string patAnalID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        bool blUpdate = false;
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(patAnalID))
                _pAnalServ.Item = new PatAnalysis();
            else
            {
                _pAnalServ.Item = await _pAnalServ.GetData(_pAnalServ.DetailsUrl, patAnalID);
                blUpdate=true;
            }
            //
            _pAnalServ.ErrorList.Clear();
            _pAnalServ.ErrorList.Add(new ErrorStatus { FieldID=PatAnalFields.Anal });
        }

        async Task SavePatAnal()
        {
            //
            pAnalMap.Map(_pAnalServ.Item, _pAnalServ.OperationItem);
            if (string.IsNullOrEmpty(patAnalID))
            {
                var newpB = await _pAnalServ.AddItem(_pAnalServ.OperationItem, _pAnalServ.AddUrl);
                if (newpB.IsSuccessStatusCode)
                    await EvHideForm.InvokeAsync(true);
            }
            else
            {
                var upPatBio =
                    await _pAnalServ.UptateItem(_pAnalServ.OperationItem, _pAnalServ.UpdateUrl);
                if (upPatBio.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pAnalServ.OperationItem.PatAnalID);
            }
        }
    }
}
