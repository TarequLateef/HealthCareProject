using AutoMapper;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBiom
{
    public partial class BioOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] BiometricService _bioServ { get; set; }
        [Inject] IMapper bioMap { get; set; }
        [Parameter] public string ExitLink { get; set; } = "/";
        [Parameter] public EventCallback<Biometrics> EvAfterSave { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await _authServ.ReadLogData();
            //
            _bioServ.Item =new Biometrics();
            _bioServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            _bioServ.ErrorList.Clear();
            _bioServ.ErrorList.Add(new ErrorStatus { FieldID=BioFields.Name });
        }

        async Task SaveBio()
        {
            _bioServ.Item.UserLogID=_authServ.Item.UserLoginID;
            _bioServ.Item.CompTypeID=_authServ.Item.CompTypeID;
            bioMap.Map(_bioServ.Item, _bioServ.OperationItem);
            var newBio = await _bioServ.AddItem(_bioServ.OperationItem, _bioServ.AddUrl);
            if (newBio.IsSuccessStatusCode)
            {
                var bioData = await newBio.Content.ReadAsStringAsync();
                _bioServ.Item= JsonConvert.DeserializeObject<Biometrics>(bioData)??new Biometrics();
                await EvAfterSave.InvokeAsync(_bioServ.Item);
            }
        }
    }
}
