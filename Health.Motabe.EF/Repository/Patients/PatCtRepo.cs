using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatCtRepo : OperationRepository<PatCt>, IPatCtRepo
    {
        readonly HealthAppContext _ctx;
        public PatCtRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;

        public async Task<IList<PatCt>> AvailableListAsync() =>
            await this.Find(pc => !pc.EndDate.HasValue || (pc.EndDate.HasValue && pc.EndDate.Value>DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.CT });

        public async Task<IList<PatCt>> BannedListAsync() =>
            await this.Find(pc => pc.EndDate.HasValue && pc.EndDate.Value<=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.CT });

        public bool IsAvalaible(PatCt Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value<=DateTime.Now);

        public async Task<PatCt> RestoreStop(PatCt sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatCtID, sItem);
        }

        public PatCt RestorStop(PatCt sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
