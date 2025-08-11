using AutoMapper;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatAnal
{
    public partial class PatAnalOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] AnalysisService _analServ { get; set; }
        [Inject] PatAnalService _pAnalServ { get; set; }
        [Inject] IMapper bioMap { get; set; }

        [Parameter] public PatAnalysis PatAnalItem { get; set; }
        [Parameter] public EventCallback<PatAnalysis> EvPatAnal { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatAnalErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        bool showAdd = false;

        protected override async Task OnInitializedAsync()
        {
            //
            _pAnalServ.CurrUserID=_authServ.Item.UserLoginID;

            //
            PatAnalItem.UserLogID=_authServ.Item.UserLoginID;
            PatAnalItem.CompID=_authServ.Item.CompID;
            PatAnalItem.BookID= BookItem.BookID;
            PatAnalItem.PatID=BookItem.PatientID;

            //
            await this.LoadBioList(new Analysis());
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
        }

        async Task LoadBioList(Analysis anal)
        {
            _analServ.OperationList = await _analServ.GetDataList(_analServ.ListUrl);
            _analServ.DataList=
                _analServ.OperationList.Where(b => b.CompTypeID==_authServ.Item.CompTypeID).ToList();
        }

        void SelectAnal(string bioID)
        {
            PatAnalItem.AnalysisID=bioID;
            this.Invoke();
        }

        void SelectResult(string mName)
        {
            PatAnalItem.AnalResult=mName;
            this.Invoke();
        }

        void SetPatDate(string dt)
        {
            PatAnalItem.StartDate=Convert.ToDateTime(dt);
            this.Invoke();
        }

        void SetPatLab(string lab)
        {
            PatAnalItem.LabName=lab;
            this.Invoke();
        }
        void Invoke()
        {
            EvPatAnal.InvokeAsync(PatAnalItem);
            EvPatAnalErr.InvokeAsync(_pAnalServ.ErrorList);
        }
    }
}
