using AutoMapper;
using Health.Motabea.Core.Models.Patients;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatientBiom
{
    public partial class PatBioOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] BiometricService _biooServ { get; set; }
        [Inject] PatBioService _pBioServ { get; set; }
        [Inject] IMapper bioMap { get; set; }

        [Parameter] public PatientBio PatBioItem { get; set; }
        [Parameter] public EventCallback<PatientBio> EvPatBio { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatBioErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        bool showAdd = false;

        protected override async Task OnInitializedAsync()
        {
            //
            _pBioServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            PatBioItem.UserLogID=_authServ.Item.UserLoginID;
            PatBioItem.CompID=_authServ.Item.CompID;
            PatBioItem.BookID= BookItem.BookID;
            PatBioItem.PatID=BookItem.PatientID;
            //
            await this.LoadBioList();
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
        }

        async Task LoadBioList()
        {
            _biooServ.OperationList = await _biooServ.GetDataList(_biooServ.ListUrl, true);
            _biooServ.DataList=
                _biooServ.OperationList.Where(b => b.CompTypeID==_authServ.Item.CompTypeID).ToList();
        }

        void SelectBio(string bioID)
        {
            PatBioItem.BioID=bioID;
            this.Invoke();
        }

        void SelectMeasure(string mName)
        {
            PatBioItem.BioMeasure=mName;
            this.Invoke();
        }

        void SetPatDate(string dt)
        {
            PatBioItem.StartDate=Convert.ToDateTime(dt);
            this.Invoke();
        }

        void SetNots(string note)
        {
            PatBioItem.Note=note;
            this.Invoke();
        }

        void Invoke()
        {
            EvPatBio.InvokeAsync(PatBioItem);
            EvPatBioErr.InvokeAsync(_pBioServ.ErrorList);
        }
    }
}
