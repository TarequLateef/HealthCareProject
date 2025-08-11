using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;

namespace Motabe.Health.BLZ.Pages.Patients.PatCts
{
    public partial class AddPatCt
    {
        [Parameter] public bool ModalForm { get; set; } = true;
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvHideForm { get; set; }
        [Inject] PatCtService _pCtServ { get; set; }
        [Inject] IMapper pCtMap { get; set; }
        [Parameter] public string patCtID { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        bool blUpdate = false;
        protected override async void OnInitialized()
        {
            if (string.IsNullOrEmpty(patCtID))
                _pCtServ.Item = new PatCt();
            else
            {
                _pCtServ.Item = await _pCtServ.GetData(_pCtServ.DetailsUrl, patCtID);
                blUpdate=true;
            }
            #region Errors
            _pCtServ.ErrorList.Clear();
            _pCtServ.ErrorList.Add(new ErrorStatus { FieldID=PatCtFields.Name });
            #endregion
        }

        async Task SavePatCt()
        {
            pCtMap.Map(_pCtServ.Item, _pCtServ.OperationItem);
            if (string.IsNullOrEmpty(patCtID))
            {
                var addPatSym = await _pCtServ.AddItem(_pCtServ.OperationItem, _pCtServ.AddUrl);
                if (addPatSym.IsSuccessStatusCode)
                    await EvHideForm.InvokeAsync(true);
            }
            else
            {
                var upPatBio =
                    await _pCtServ.UptateItem(_pCtServ.OperationItem, _pCtServ.UpdateUrl);
                if (upPatBio.IsSuccessStatusCode)
                    await EvUpdateItem.InvokeAsync(_pCtServ.OperationItem.PatCtID);
            }
        }
    }
}
