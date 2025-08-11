using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatCts
{
    public partial class PatCtList
    {
        [Inject] PatCtService _pCtServ { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Parameter] public DateTime CtDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }
        bool showHist = true;
        string currPatCtID = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _pCtServ.CurrUserID=_authServ.Item.UserLoginID;
            _pCtServ.FieldsPerPage=4;
            _pCtServ.MainList=
                await _pCtServ.GetDataList(_pCtServ.ListUrl+PatCtFields.ParamPat+PatItem.PatientID);
            this.GetHistory(true);
        }
        void GetHistory(bool hist)
        {
            showHist=hist;
            if (!hist)
                _pCtServ.OperationList =
                    _pCtServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<CtDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                _pCtServ.OperationList=
                    _pCtServ.MainList.Where(pb => pb.CompID==_authServ.Item.CompID && pb.StartDate.Date==CtDate.Date).ToList();

            _pCtServ.ChangePage(1);
        }

        async Task DeleteBio(string sypID)
        {
            await _pCtServ.DeleteItem(_pCtServ.DeleteUrl, sypID);
            _pCtServ.MainList=
                await _pCtServ.GetDataList(_pCtServ.ListUrl+PatBioFields.PatBioParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatCtID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pCtServ.MainList.First(ps => ps.PatCtID==itemID);
            int itemIndx = _pCtServ.MainList.IndexOf(pSyItem);
            _pCtServ.Item = await _pCtServ.GetData(_pCtServ.DetailsUrl, itemID);
            _pCtServ.MainList[itemIndx]=_pCtServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }

    }
}
