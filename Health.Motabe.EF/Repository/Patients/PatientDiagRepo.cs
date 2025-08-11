using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
public    class PatientDiagRepo:OperationRepository<PatientDiag>, IPatientDiagRepo
    {
        protected readonly HealthAppContext _ctx;
        public PatientDiagRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;

        public async Task<IList<PatientDiag>> AvailableListAsync() =>
            await this.Find(pd => !pd.EndDate.HasValue || (pd.EndDate.HasValue && pd.EndDate.Value<DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Diagnostic });

        public async Task<IList<PatientDiag>> BannedListAsync() =>
            await this.Find(pd => pd.EndDate.HasValue && pd.EndDate.Value>=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Diagnostic });

        public bool IsAvalaible(PatientDiag Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value<DateTime.Now);

        public async Task<PatientDiag> RestoreStop(PatientDiag sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatDiagID, sItem);
        }

        public PatientDiag RestorStop(PatientDiag sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
