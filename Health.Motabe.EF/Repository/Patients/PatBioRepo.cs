using GeneralMotabea.Core.General.DbStructs;
using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatBioRepo:OperationRepository<PatientBio>,IPatBioRepo
    {
        readonly HealthAppContext _ctx;
        public PatBioRepo(HealthAppContext ctx) : base(ctx) => _ctx = ctx;

        public async Task<IList<PatientBio>> AvailableListAsync() =>
            await this.Find(pb => !pb.EndDate.HasValue || (pb.EndDate.HasValue && pb.EndDate.Value>DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Biometirc });

        public async Task<IList<PatientBio>> BannedListAsync() =>
            await this.Find(pb => pb.EndDate.HasValue && pb.EndDate.Value<=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Biometirc });

        public bool IsAvalaible(PatientBio Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value>DateTime.Now);

        public async Task<PatientBio> RestoreStop(PatientBio sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatBioID, sItem);
        }

        public PatientBio RestorStop(PatientBio sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
