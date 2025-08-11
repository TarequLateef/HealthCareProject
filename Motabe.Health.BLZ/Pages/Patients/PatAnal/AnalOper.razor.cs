using AutoMapper;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatAnal
{
    public partial class AnalOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] AnalysisService _analServ { get; set; }
        [Inject] IMapper analMap { get; set; }
        [Parameter] public string ExitLink { get; set; } = "/";
        [Parameter] public EventCallback<Analysis> EvAfterSave { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            //
            _analServ.Item =new Analysis();
            _analServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _analServ.ErrorList.Clear();
            _analServ.ErrorList.Add(new ErrorStatus { FieldID=AnalFields.Name });
        }

        async Task Saveanal()
        {
            _analServ.Item.UserLogID=_authServ.Item.UserLoginID;
            _analServ.Item.CompTypeID=_authServ.Item.CompTypeID;
            analMap.Map(_analServ.Item, _analServ.OperationItem);
            var newanal = await _analServ.AddItem(_analServ.OperationItem, _analServ.AddUrl);
            if (newanal.IsSuccessStatusCode)
            {
                var analData = await newanal.Content.ReadAsStringAsync();
                _analServ.Item= JsonConvert.DeserializeObject<Analysis>(analData)??new Analysis();
                await EvAfterSave.InvokeAsync(_analServ.Item);
            }
        }

    }
}
