using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatMeds
{
    public partial class PatMedList
    {
        [Inject] PatMedService _pMedServ { get; set; }
        [Inject] AutherService _authServ { get; set; }
        [Parameter] public DateTime MedDate { get; set; }
        [Parameter] public PatientBaseData PatItem { get; set; }
        [Parameter] public Booking PatBookItem { get; set; }
        [Parameter] public EventCallback<bool> EvShowAdd { get; set; }
        bool showHist = true;
        string currPatTreatID = string.Empty;
        IList<PatMedGroup> pmDateGr = new List<PatMedGroup>();
        protected override async Task OnInitializedAsync()
        {
            _pMedServ.CurrUserID=_authServ.Item.UserLoginID;
            _pMedServ.FieldsPerPage=3;
            _pMedServ.MainList=
                await _pMedServ.GetDataList(_pMedServ.ListUrl+PatMedFields.ParamPat+PatItem.PatientID);
            this.GetHistory(true);
        }
        void GetHistory(bool hist)
        {
            IList<PatMed> pmList = new List<PatMed>();
            if (!hist)
                pmList = 
                    _pMedServ.MainList.Where(ps => ps.CompID==_authServ.Item.CompID && ps.StartDate.Date<MedDate.Date)
                    .OrderByDescending(ps => ps.StartDate).ToList();
            else
                pmList=
                    _pMedServ.MainList.Where(pb => pb.CompID==_authServ.Item.CompID && pb.StartDate.Date==MedDate.Date).ToList();

            pmDateGr = (from pm in pmList
                            group pm by pm.StartDate.Date into pmDate
                            orderby pmDate.Key descending
                            select new PatMedGroup
                            {
                                MedDate=pmDate.Key.Date,
                                CountOfMed=pmList.Count(p => p.StartDate.Date==pmDate.Key.Date)
                            }).ToList();

            _pMedServ.OperationList = pmList;
            _pMedServ.ChangePage(1);
        }

        async Task DeleteBio(string sypID)
        {
            await _pMedServ.DeleteItem(_pMedServ.DeleteUrl, sypID);
            _pMedServ.MainList=
                await _pMedServ.GetDataList(_pMedServ.ListUrl+PatMedFields.ParamPat+PatItem.PatientID);
            this.GetHistory(showHist);
        }

        void SelectUpdate(string itemID) => currPatTreatID=itemID;

        async Task ReplaceItem(string itemID)
        {
            var pSyItem = _pMedServ.MainList.First(ps => ps.PatMedID==itemID);
            int itemIndx = _pMedServ.MainList.IndexOf(pSyItem);
            _pMedServ.Item = await _pMedServ.GetData(_pMedServ.DetailsUrl, itemID);
            _pMedServ.MainList[itemIndx]=_pMedServ.Item;
            this.GetHistory(showHist);
            await EvShowAdd.InvokeAsync(false);
        }

    }
}
