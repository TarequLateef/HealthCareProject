using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatRay
{
    public partial class PatRayList
    {
        [Inject] PatRayService _pRayServ { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Parameter] public DateTime RayDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }
        bool showHist = true;
        string currPatRayID = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _pRayServ.CurrUserID=_authServ.Item.UserLoginID;
            _pRayServ.FieldsPerPage=4;
            _pRayServ.MainList=
                await _pRayServ.GetDataList(_pRayServ.ListUrl+PatRayFields.ParamPat+PatItem.PatientID);
            this.GetHistory(true);
        }
        void GetHistory(bool hist)
        {
            if (!hist)
                _pRayServ.OperationList =
                    _pRayServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<RayDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                _pRayServ.OperationList=
                    _pRayServ.MainList.Where(pb => pb.CompID==_authServ.Item.CompID && pb.StartDate.Date==RayDate.Date).ToList();

            _pRayServ.ChangePage(1);
        }

        async Task DeleteBio(string sypID)
        {
            await _pRayServ.DeleteItem(_pRayServ.DeleteUrl, sypID);
            _pRayServ.MainList=
                await _pRayServ.GetDataList(_pRayServ.ListUrl+PatBioFields.PatBioParam+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatRayID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pRayServ.MainList.First(ps => ps.PRID==itemID);
            int itemIndx = _pRayServ.MainList.IndexOf(pSyItem);
            _pRayServ.Item = await _pRayServ.GetData(_pRayServ.DetailsUrl, itemID);
            _pRayServ.MainList[itemIndx]=_pRayServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }

    }
}
