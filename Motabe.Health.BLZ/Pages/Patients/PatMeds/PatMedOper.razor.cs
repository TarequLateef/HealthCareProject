using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatMeds
{
    public partial class PatMedOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] MedicineService _medServ { get; set; }
        [Inject] PatMedService _pMedServ { get; set; }
        [Inject] IMapper medMap { get; set; }

        [Parameter] public PatMed PatMedItem { get; set; }
        [Parameter] public EventCallback<PatMed> EvPatMed { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatMedErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        bool showAdd = false;
        string[] MedPer = { MedPeriods.Hour, MedPeriods.Day };
        protected override async Task OnInitializedAsync()
        {
            //
            _pMedServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            PatMedItem.UserLogID=_authServ.Item.UserLoginID;
            PatMedItem.CompID=_authServ.Item.CompID;
            PatMedItem.BookID= BookItem.BookID;
            PatMedItem.PatID=BookItem.PatientID;

            //
            await this.LoadMedList(new Medicine());
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
        }

        async Task LoadMedList(Medicine med)
        {
            _medServ.OperationList = await _medServ.GetDataList(_medServ.ListUrl);
            _medServ.DataList=
                _medServ.OperationList.Where(b => b.CompTypeID==_authServ.Item.CompTypeID).ToList();
        }

        void SelectMed(string bioID)
        {
            PatMedItem.MedID=bioID;
            this.Invoke();
        }

        void SelectDose(string mDose)
        {
            PatMedItem.Dose=mDose;
            this.Invoke();
        }

        void SetPatDate(string dt)
        {
            PatMedItem.StartDate=Convert.ToDateTime(dt);
            this.Invoke();
        }

        void SetPatPeriod(string per)
        {
            PatMedItem.Period=per;
            this.Invoke();
        }

        void SetPatMedNote(string note)
        {
            PatMedItem.Note = note;
            this.Invoke();
        }
        void Invoke()
        {
            EvPatMed.InvokeAsync(PatMedItem);
            EvPatMedErr.InvokeAsync(_pMedServ.ErrorList);
        }
    }
}
