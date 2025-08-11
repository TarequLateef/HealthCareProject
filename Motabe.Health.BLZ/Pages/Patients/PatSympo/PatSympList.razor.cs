using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatSympo
{
    public partial class PatSympList
    {
        [Inject] PatientSympService _pSympServ { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Parameter] public DateTime SympDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }

        bool showHist = true;
        string currPatSypID = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _pSympServ.FieldsPerPage=4;
            _pSympServ.MainList=
                await _pSympServ.GetDataList(_pSympServ.ListUrl+PatSysmpFields.PatParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void GetHistory(bool hist)
        {
            showHist=hist;
            if (!hist)
                _pSympServ.OperationList =
                    _pSympServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<SympDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                _pSympServ.OperationList =
                _pSympServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date==SympDate.Date)
                .OrderByDescending(ps => ps.StartDate).ToList();

            _pSympServ.ChangePage(1);
        }

        async Task DeleteSyp(string sypID)
        {
            await _pSympServ.DeleteItem(_pSympServ.DeleteUrl, sypID);
            _pSympServ.MainList=
                await _pSympServ.GetDataList(_pSympServ.ListUrl+PatSysmpFields.PatParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatSypID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pSympServ.MainList.First(ps => ps.PatSympID==itemID);
            int itemIndx = _pSympServ.MainList.IndexOf(pSyItem);
            _pSympServ.Item = await _pSympServ.GetData(_pSympServ.DetailsUrl, itemID);
            _pSympServ.MainList[itemIndx]=_pSympServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }
    }
}
