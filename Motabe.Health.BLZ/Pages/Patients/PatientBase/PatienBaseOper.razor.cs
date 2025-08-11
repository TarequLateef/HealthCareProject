using AutoMapper;
using GeneralMotabea.Core.DTOs;
using GeneralMotabea.Core.General;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Adress;
using Motabe.Health.BLZ.Data.Services.ExternalData;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;
using Motabe.Health.BLZ.Shared;
using Newtonsoft.Json;
using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBase
{
    public partial class PatienBaseOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] PatientBaseService _pBaseServ { get; set; }
        /*[Inject] PatientDetailsService _pDetServ { get; set; }*/
        [Inject] CountryService _countryServ { get; set; }
        [Inject] GovernService _govServ { get; set; }
        [Inject] CityService _cityServ { get; set; }
        [Inject] WorkService _workServ { get; set; }
        [Inject] IMapper _pDetMap { get; set; }

        /*[Parameter] public PatientData PatDetItem { get; set; }*/
        [Parameter] public PatientBaseData PatBaseItem { get; set; }
        [Parameter] public EventCallback<PatientBaseData> EvPatItem { get; set; }
        /*[Parameter] public EventCallback<PatientData> EvPatDetItem { get; set; }*/
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatErrList { get; set; }
        /*[Parameter] public EventCallback<IList<ErrorStatus>> EvPatDetErrList { get; set; }*/
        [Parameter] public EventCallback<string> EvGovernCode { get; set; }
        [Parameter] public EventCallback<string> EvCityCode { get; set; }
        
        ErrorStatus phoneErr = new ErrorStatus();
        bool searchGovern = false;
        Genders gen = new Genders();
        string Age = "0";
        bool docRole = true;
        protected override async Task OnInitializedAsync()
        {

            docRole= _authServ.Item.Roles.Any(role => role==RolesList.Doc);
            if (!string.IsNullOrEmpty(PatBaseItem.PatientID))
                Age=(DateTime.Now.Year-PatBaseItem.AgeDate.Year).ToString();
            else
                PatBaseItem.CountryID="01";

            _govServ.OperationList=
                await _govServ.GetDataList(_govServ.ListUrl+"&cID="+PatBaseItem.CountryID);
            await this.GetGovernList(searchGovern);
            //
            _countryServ.OperationList = await _countryServ.GetDataList(_countryServ.ListUrl);
            _countryServ.DataList = _countryServ.OperationList;
            //
            await this.LoadWork();
        }

        async Task LoadWork()
        {
            // 
            _workServ.OperationList = await _workServ.GetDataList(_workServ.ListUrl);
            _workServ.DataList = _workServ.OperationList;
        }

        void SetName(string name)
        {
            PatBaseItem.PatientName = name;
            EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        async Task SetPhone(string phone)
        {
            PatBaseItem.Phone=phone;
            _pBaseServ.ReturnItem = await _pBaseServ.ExistPhone(phone);
            var phErr = _pBaseServ.ErrorList.First(ue => ue.FieldID==PatBaseFields.Phone);
            if (!phErr.Error)
            {
                this.phoneErr = new ErrorStatus
                {
                    Done = _pBaseServ.ReturnItem.Status,
                    Error=!_pBaseServ.ReturnItem.Status,
                    ErrorMessage=!_pBaseServ.ReturnItem.Status ? "متابعة مريض آخر" : "",
                    FieldID=PatBaseFields.Phone
                };
                _pBaseServ.ReloadErrors(this.phoneErr);
            }
            await EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        #region Address
        async Task SelectGover(string govID)
        {
            PatBaseItem.GovernID=govID;
            string code = _govServ.OperationList.First(g => g.ConvernID==govID).GovCode;
            //
            _cityServ.OperationList = await _cityServ.GetDataList(_cityServ.ListUrl+"&gID="+govID);
            _cityServ.DataList = _cityServ.OperationList;
            //
            await EvGovernCode.InvokeAsync(code);
            await EvPatItem.InvokeAsync(PatBaseItem);
            await EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        async Task GetGovernList(bool en)
        {
            searchGovern=en;
            _govServ.DataList=_govServ.OperationList;
            await SelectGover("07");
            await EvPatItem.InvokeAsync(PatBaseItem);
            await EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        void SelectCity(string cityID)
        {
            //
            PatBaseItem.CityID=cityID;
            //
            string code = _cityServ.OperationList.First(c => c.CityID==cityID).CityCode;
            EvCityCode.InvokeAsync(code);
            EvPatItem.InvokeAsync(PatBaseItem);
            EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }
        #endregion

        #region Other Data
        void SetAge(string g)
        {
            Age=g;
            PatBaseItem.AgeDate=
                new DateTime(DateTime.Now.Year-Convert.ToInt32(g), DateTime.Now.Month, DateTime.Now.Day);
            EvPatItem.InvokeAsync(PatBaseItem);
            EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        void GetGender(string gen)
        {
            PatBaseItem.Gender=gen;
            EvPatItem.InvokeAsync(PatBaseItem);
            EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        void SelectWork(string wID)
        {
            PatBaseItem.WorkID=wID;
            EvPatItem.InvokeAsync(PatBaseItem);
            EvPatErrList.InvokeAsync(_pBaseServ.ErrorList);
        }

        async Task AddWork(string wName)
        {
            string workCode =
                new MathConvertion(MathSystem.Decimal, _workServ.OperationList.Count-1).ReturnedNumber;
            workCode = new SerialCode(2, Convert.ToInt32(workCode)).GenerateCode;
            _workServ.Item = new Work
            {
                UserLogID=_authServ.Item.UserLoginID,
                WorkName=wName,
                WorkCode=workCode
            };
            _pDetMap.Map(_workServ.Item, _workServ.OperationItem);
            var newWork = await _workServ.AddItem(_workServ.OperationItem, _workServ.AddUrl);
            if (newWork.IsSuccessStatusCode)
            {
                var workData = await newWork.Content.ReadAsStringAsync();
                _workServ.Item = JsonConvert.DeserializeObject<Work>(workData)??new Work();
                if (!_workServ.OperationList.Any(w => w.WorkID==_workServ.Item.WorkID))
                    _workServ.OperationList.Add(_workServ.Item);
                _workServ.DataList=_workServ.OperationList;
                _pBaseServ.ErrorList.First(p => p.FieldID==PatDataFields.Work).Done=true;
                this.SelectWork(_workServ.Item.WorkID);
            }
        }
        #endregion
    }
}
