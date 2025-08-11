using AutoMapper;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatMeds
{
    public partial class MedOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] MedicineService _medServ { get; set; }
        [Inject] IMapper medMap { get; set; }
        [Parameter] public string ExitLink { get; set; } = "/";
        [Parameter] public EventCallback<Medicine> EvAfterSave { get; set; }

        string[] MedList = { MedTypes.Caps, MedTypes.Soup, MedTypes.Inj, MedTypes.Inh };
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            //
            _medServ.Item =new Medicine();
            _medServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _medServ.ErrorList.Clear();
            _medServ.ErrorList.Add(new ErrorStatus { FieldID=MedFields.Name });
            _medServ.ErrorList.Add(new ErrorStatus { FieldID=MedFields.Type });
        }

        async Task SaveMed()
        {
            _medServ.Item.UserLogID=_authServ.Item.UserLoginID;
            _medServ.Item.CompTypeID=_authServ.Item.CompTypeID;
            medMap.Map(_medServ.Item, _medServ.OperationItem);
            var newanal = await _medServ.AddItem(_medServ.OperationItem, _medServ.AddUrl);
            if (newanal.IsSuccessStatusCode)
            {
                var analData = await newanal.Content.ReadAsStringAsync();
                _medServ.Item= JsonConvert.DeserializeObject<Medicine>(analData)??new Medicine();
                await EvAfterSave.InvokeAsync(_medServ.Item);
            }
        }

    }
}
