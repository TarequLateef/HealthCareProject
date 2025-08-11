using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBiom
{
    public partial class PatBioList
    {
        [Inject] AutherService _authServ {  get; set; }
        [Inject] PatBioService _pBioServ { get; set; }
        [Parameter] public DateTime BioDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }
        bool showHist = true;
        string currPatBioID = string.Empty;

        protected override async Task OnInitializedAsync()
        {
           await _authServ.ReadLogData();
            _pBioServ.CurrUserID=_authServ.Item.UserLoginID;
            _pBioServ.FieldsPerPage=4;
            _pBioServ.MainList = 
                await _pBioServ.GetDataList(_pBioServ.ListUrl+PatBioFields.PatBioParam+PatItem.PatientID);
            
            this.GetHistory(true);
        }
        void GetHistory(bool hist)
        {
            showHist=hist;
            if (!hist)
                _pBioServ.OperationList =
                    _pBioServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<BioDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                _pBioServ.OperationList=
                    _pBioServ.MainList.Where(pb => pb.CompID==_authServ.Item.CompID && pb.StartDate.Date==BioDate.Date).ToList();

            _pBioServ.ChangePage(1);
        }

        async Task DeleteBio(string sypID)
        {
            await _pBioServ.DeleteItem(_pBioServ.DeleteUrl, sypID);
            _pBioServ.MainList=
                await _pBioServ.GetDataList(_pBioServ.ListUrl+PatBioFields.PatBioParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatBioID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pBioServ.MainList.First(ps => ps.PatBioID==itemID);
            int itemIndx = _pBioServ.MainList.IndexOf(pSyItem);
            _pBioServ.Item = await _pBioServ.GetData(_pBioServ.DetailsUrl, itemID);
            _pBioServ.MainList[itemIndx]=_pBioServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }

    }
}
