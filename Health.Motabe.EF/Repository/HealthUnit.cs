using GeneralMotabea.Core.Interfaces;
using Health.Motabe.EF.Repository;
using Health.Motabe.EF.Repository.Patients;
using Health.Motabea.Core;
using Health.Motabea.Core.Interfaces;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using System.Diagnostics;
using UserManagement.Core.Models.Address;

namespace Health.Motabea.EF.Repositories
{
    public class HealthUnit : IHealthUnits
    {
        readonly HealthAppContext _ctx;

        #region Patient
        public IBookRepo Booking { get; private set; }
        public IPatientDataRepo PatientBaseData { get; private set; }
        public IOperationRepository<PatientData> PatientData { get; private set; }

        public IPatientDiagRepo PatientDiag { get; private set; }
        public IPatientSympRepo PatientSymp { get; private set; }
       public IPatBioRepo PatBio { get; }
        public IPatRayRepo PatRay { get; }
        public IPatAnalysisRepo PatAnalysis { get; }
        public IPatMedRepo PatMed { get; }
        public IPatCtRepo PatCt { get; }
        #endregion

        #region Service
        public IOperationRepository<Diagnostic> Diagnositic { get; private set; }
        public IOperationRepository<Symptoms> Symptoms { get; private set; }
        public IOperationRepository<Biometrics> Biometric {  get; private set; }
       public IOperationRepository<CT> CT { get; private set; }
        public IOperationRepository<Medicine> Medicine { get; private set; }
       public IOperationRepository<Analysis> Analysis { get; private set; }
       public IOperationRepository<Rays> Rays { get; private set; }

        #endregion
        public IUserDataForAppRepo UserData { get; private set; }
        public IWorkReop Work { get; private set; }
        public HealthUnit(HealthAppContext context)
        {
            _ctx = context;
            #region Patient
            this.Booking = new BookingRepo(_ctx);
            this.PatientBaseData = new PatientDataRepo(_ctx);
            this.PatientData=new OperationRepository<PatientData>(_ctx);
            this.PatientDiag = new PatientDiagRepo(_ctx);
            this.PatientSymp = new PatientSympRepo(_ctx);
            this.PatBio = new PatBioRepo(_ctx);
            this.PatRay=new PatRayRepo(_ctx);
            this.PatAnalysis=new PatAnalysisRepo(_ctx);
            this.PatMed=new PatMedRepo(_ctx);
            this.PatCt=new PatCtRepo(_ctx);
            #endregion
            #region Service
            this.Diagnositic = new OperationRepository<Diagnostic>(_ctx);
            this.Symptoms = new OperationRepository<Symptoms>(_ctx);
            this.Biometric=new OperationRepository<Biometrics>(_ctx);
            this.CT = new OperationRepository<CT>(_ctx);
            this.Medicine = new OperationRepository<Medicine>(_ctx);
            this.Analysis = new OperationRepository<Analysis>(_ctx);
            this.Rays = new OperationRepository<Rays>(_ctx);
            #endregion
            this.UserData = new UserDataRepo(_ctx);
            this.Work = new WorkRepo(_ctx);
        }


        #region Saving
        public void Dispose() => _ctx.Dispose();

        public void Submit() => _ctx.SaveChanges();

        public async Task SubmitAsync() => await _ctx.SaveChangesAsync();
        #endregion
    }
}
