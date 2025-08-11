using AutoMapper;
using GeneralMotabea.Core.General;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Adress;
using Motabe.Health.BLZ.Data.Services.ExternalData;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;
using UserManagement.Core.DTOs.Comp;
using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Pages.Patients.PatientsData
{
    public partial class PatientDataOpser
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatientDetailsService _pDetServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }

        [Parameter] public PatientBaseData PatientItem { get; set; }
        [Parameter] public EventCallback<PatientData> EvPatItem { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvErrList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _pDetServ.MainList = await _pDetServ.GetDataList(_pDetServ.ListUrl);
            if (_pDetServ.MainList.Any(pd => pd.PatientID==PatientItem.PatientID))
                _pDetServ.Item = _pDetServ.MainList.First(pd => pd.PatientID==PatientItem.PatientID);
        }
    }
}
