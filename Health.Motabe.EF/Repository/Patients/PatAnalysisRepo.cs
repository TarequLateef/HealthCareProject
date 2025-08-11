using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatAnalysisRepo : OperationRepository<PatAnalysis>, IPatAnalysisRepo
    {
        readonly HealthAppContext _ctx;
        public PatAnalysisRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;

        public async Task<IList<PatAnalysis>> AvailableListAsync() =>
               await this.Find(pa => !pa.EndDate.HasValue || (pa.EndDate.HasValue && pa.EndDate.Value>DateTime.Now), new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Analysis });
        public async Task<IList<PatAnalysis>> BannedListAsync() =>
            await this.Find(pa => pa.EndDate.HasValue && pa.EndDate.Value<=DateTime.Now, new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Analysis });

        public bool IsAvalaible(PatAnalysis Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value>DateTime.Now);

        public async Task<PatAnalysis> RestoreStop(PatAnalysis sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.PatAnalID, sItem);
        }

        public PatAnalysis RestorStop(PatAnalysis sItem)
        {
            sItem.EndDate = sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
