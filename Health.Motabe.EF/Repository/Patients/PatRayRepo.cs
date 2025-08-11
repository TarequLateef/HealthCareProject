using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatRayRepo:OperationRepository<PatientRays>,IPatRayRepo
    {
        readonly HealthAppContext _ctx;
        public PatRayRepo(HealthAppContext ctx) : base(ctx) => _ctx = ctx;

        public async Task<IList<PatientRays>> AvailableListAsync()=>
            await this.Find(pr=>!pr.EndDate.HasValue || (pr.EndDate.HasValue && pr.EndDate.Value>DateTime.Now), new[] {PatientTab.Patient,PatientTab.Booking,ServTab.Rays});

        public async Task<IList<PatientRays>> BannedListAsync() =>
            await this.Find(pr => pr.EndDate.HasValue && pr.EndDate.Value<=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Rays });
        public bool IsAvalaible(PatientRays Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value>DateTime.Now);

        public async Task<PatientRays> RestoreStop(PatientRays sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PRID, sItem);
        }

        public PatientRays RestorStop(PatientRays sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
