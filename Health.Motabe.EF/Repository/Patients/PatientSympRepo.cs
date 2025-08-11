using GeneralMotabea.Core.General.DbStructs;
using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatientSympRepo:OperationRepository<PatientSymptom>,IPatientSympRepo
    {
        protected readonly HealthAppContext _ctx;
        public PatientSympRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;

        public async Task<IList<PatientSymptom>> AvailableListAsync() =>
            await this.Find(ps => !ps.EndDate.HasValue || (ps.EndDate.HasValue && ps.EndDate.Value<DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Symptoms });

        public async Task<IList<PatientSymptom>> BannedListAsync() =>
            await this.Find(ps => ps.EndDate.HasValue && ps.EndDate.Value>=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Symptoms });

        public bool IsAvalaible(PatientSymptom Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value<DateTime.Now);

        public async Task<PatientSymptom> RestoreStop(PatientSymptom sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatSympID, sItem);
        }

        public PatientSymptom RestorStop(PatientSymptom sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
