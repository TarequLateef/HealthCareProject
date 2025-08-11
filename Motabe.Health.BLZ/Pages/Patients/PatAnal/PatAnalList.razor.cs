using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatAnal
{
    public partial class PatAnalList
    {
        [Inject] PatAnalService _pAnalServ { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Parameter] public DateTime AnalDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }
        bool showHist = true;
        string currPatAnalID = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _pAnalServ.CurrUserID=_authServ.Item.UserLoginID;
            _pAnalServ.FieldsPerPage=4;
            _pAnalServ.MainList=
                await _pAnalServ.GetDataList(_pAnalServ.ListUrl+PatAnalFields.PatParam+PatItem.PatientID);
            this.GetHistory(true);
        }
        void GetHistory(bool hist)
        {
            if (!hist)
                _pAnalServ.OperationList =
                    _pAnalServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<AnalDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                _pAnalServ.OperationList=
                    _pAnalServ.MainList.Where(pb => pb.CompID==_authServ.Item.CompID && pb.StartDate.Date==AnalDate.Date).ToList();

            _pAnalServ.ChangePage(1);
        }

        async Task DeleteBio(string sypID)
        {
            await _pAnalServ.DeleteItem(_pAnalServ.DeleteUrl, sypID);
            _pAnalServ.MainList=
                await _pAnalServ.GetDataList(_pAnalServ.ListUrl+PatBioFields.PatBioParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatAnalID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pAnalServ.MainList.First(ps => ps.PatAnalID==itemID);
            int itemIndx = _pAnalServ.MainList.IndexOf(pSyItem);
            _pAnalServ.Item = await _pAnalServ.GetData(_pAnalServ.DetailsUrl, itemID);
            _pAnalServ.MainList[itemIndx]=_pAnalServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }

    }
}
