using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatMedRepo : OperationRepository<PatMed>, IPatMedRepo
    {
        readonly HealthAppContext _ctx;
        public PatMedRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;


        public async Task<IList<PatMed>> AvailableListAsync() =>
            await this.Find(pm => !pm.EndDate.HasValue || (pm.EndDate.HasValue && pm.EndDate.Value<DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Medicine });

        public async Task<IList<PatMed>> BannedListAsync() =>
            await this.Find(pm => pm.EndDate.HasValue && pm.EndDate.Value>=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Medicine });

        public bool IsAvalaible(PatMed Item) =>
            !Item.EndDate.HasValue ||(Item.EndDate.HasValue && Item.EndDate.Value>=DateTime.Now);

        public async Task<PatMed> RestoreStop(PatMed sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatMedID, sItem);
        }

        public PatMed RestorStop(PatMed sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }

        public async Task<IList<PatMed>> SpecPatMed(string patID, DateTime PatMedDate) => 
            await this.Find(p => p.PatID==patID && p.StartDate.Date==PatMedDate.Date);

    }
}
