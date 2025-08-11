using AutoMapper;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatRay
{
    public partial class RayOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] RaysService _rayServ { get; set; }
        [Inject] IMapper rayMap { get; set; }
        [Parameter] public string ExitLink { get; set; } = "/";
        [Parameter] public EventCallback<Rays> EvAfterSave { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            //
            _rayServ.Item =new Rays();
            _rayServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _rayServ.ErrorList.Clear();
            _rayServ.ErrorList.Add(new ErrorStatus { FieldID=RayFields.Name });
        }

        async Task Saveanal()
        {
            _rayServ.Item.UserLogID=_authServ.Item.UserLoginID;
            _rayServ.Item.CompTypeID=_authServ.Item.CompTypeID;
            rayMap.Map(_rayServ.Item, _rayServ.OperationItem);
            var newanal = await _rayServ.AddItem(_rayServ.OperationItem, _rayServ.AddUrl);
            if (newanal.IsSuccessStatusCode)
            {
                var analData = await newanal.Content.ReadAsStringAsync();
                _rayServ.Item= JsonConvert.DeserializeObject<Rays>(analData)??new Rays();
                await EvAfterSave.InvokeAsync(_rayServ.Item);
            }
        }

    }
}
