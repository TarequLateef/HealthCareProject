using AutoMapper;
using GeneralMotabea.Core.General;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBase
{
    public partial class AddPatientBase
    {
        [Inject] PatientBaseService _pBaseServ { get; set; }
        [Inject] NavigationManager _pNav { get; set; }
        [Inject] IMapper _pMap { get; set; }
        [Inject] AutherService _authServ { get; set; }

        [Parameter] public bool ShowAsModal { get; set; } = false;
        [Parameter] public string ExitLink { get; set; } = "/";
        [Parameter] public EventCallback<PatientBaseData> EvAfterSave { get; set; }
        [Parameter] public PatientBaseData PatientItem { get; set; }

        string cityCode = string.Empty;
        string governCode = string.Empty;
        protected override void OnInitialized()
        {
            this.LoadOperation();
        }

        void LoadOperation()
        {
            if (PatientItem is null)
                _pBaseServ.Item = new PatientBaseData();
            else
                _pBaseServ.Item = PatientItem;
            _pBaseServ.OperationItem = new PatientBaseDTO();
            _pBaseServ.Item.CompID = _authServ.Item.CompID;
            _pBaseServ.Item.UserLogID=_authServ.Item.UserLoginID;
            //
            #region Errors
            _pBaseServ.ErrorList.Clear();
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=PatBaseFields.Name });
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=PatBaseFields.Phone });
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=PatDataFields.Work });
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=AddressFields.City });
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=PatDataFields.Age });
            _pBaseServ.ErrorList.Add(new ErrorStatus { FieldID=PatDataFields.Gender });
            #endregion
        }

        void LoadPatientCdoe()
        {
            DateTime ageDate = _pBaseServ.Item.AgeDate;
            string patCode = new MathConvertion(MathSystem.Hexa, ageDate.Year).ReturnedNumber
                +SerialCode.CovnertCode(3, cityCode)
                +SerialCode.CovnertCode(2, governCode);
            patCode+=_pBaseServ.Item.Gender==Genders.EnMale || _pBaseServ.Item.Gender== Genders.ArMale ? "1" : "0";
            int codeRep = _pBaseServ.MainList.Count(p => p.PatientCode.Substring(0, 8)==patCode);
            patCode+=SerialCode.CovnertCode(4, (codeRep+1).ToString());
            _pBaseServ.Item.PatientCode=patCode;
        }

        async Task SavePatient()
        {
            this.LoadPatientCdoe();
            _pMap.Map(_pBaseServ.Item, _pBaseServ.OperationItem);
            if (string.IsNullOrEmpty(_pBaseServ.Item.PatientID))
            {
                var pItem = await _pBaseServ.AddItem(_pBaseServ.OperationItem, _pBaseServ.AddUrl);
                if (pItem.IsSuccessStatusCode)
                {
                    var newPItem = await pItem.Content.ReadAsStringAsync();
                    _pBaseServ.Item = JsonConvert.DeserializeObject<PatientBaseData>(newPItem)??
                        new PatientBaseData();
                    if (ExitLink!="/")
                        await EvAfterSave.InvokeAsync(_pBaseServ.Item);
                    this.LoadOperation();
                }
            }
            else
            {
                var upPat = 
                    await _pBaseServ.UptateItem(_pBaseServ.OperationItem, _pBaseServ.UpdateUrl);
                if (upPat.IsSuccessStatusCode)
                {
                    var upPtItem = await upPat.Content.ReadAsStringAsync();
                    _pBaseServ.Item = JsonConvert.DeserializeObject<PatientBaseData>(upPtItem)?? 
                        new PatientBaseData();
                    if (ExitLink!="/")
                        await EvAfterSave.InvokeAsync(_pBaseServ.Item);
                }
            }
        }

        void ExitCreate(string link)
        {
            this.LoadOperation();
            _pNav.NavigateTo(link);
        }
    }
}
