using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Adress;
using Motabe.Health.BLZ.Data.Services.ExternalData;
using Motabe.Health.BLZ.Data.Services.Patients;
using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Pages.Patients.PatientsData
{
    public partial class PatientDataInfo
    {
        [Inject] PatientDetailsService _pDetServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] GovernService _gServ { get; set; }
        [Inject] CityService _cityServ { get; set; }
        [Inject] WorkService _wServ { get; set; }
        [Parameter] public string pdID { get; set; }
        [Parameter] public EventCallback<bool> EvDone { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }

        string upId = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _pBaseServ.Item = await _pBaseServ.GetData(_pBaseServ.DetailsUrl, pdID);
            //
            _pDetServ.Item = await _pDetServ.GetData(_pDetServ.DetailsUrl, pdID);
            _gServ.Item = string.IsNullOrEmpty(_pBaseServ.Item.GovernID) ? new Govern()
                : await _gServ.GetData(_gServ.DetailsUrl, _pBaseServ.Item.GovernID);
            _cityServ.Item = string.IsNullOrEmpty(_pBaseServ.Item.CityID) ? new City()
                : await _cityServ.GetData(_cityServ.DetailsUrl, _pBaseServ.Item.CityID);
            _wServ.Item = string.IsNullOrEmpty(_pBaseServ.Item.WorkID) ? new Work()
                : await _wServ.GetData(_wServ.DetailsUrl, _pBaseServ.Item.WorkID);
        }
    }
}
