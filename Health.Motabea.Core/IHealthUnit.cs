using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Interfaces;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;

namespace Health.Motabea.Core
{
    public interface IHealthUnits:IDisposable
    {
        #region Patients
        IBookRepo Booking { get; }
        IPatientDataRepo PatientBaseData { get; }
        IOperationRepository<PatientData> PatientData { get; }
        IPatientDiagRepo PatientDiag { get; }
        IPatientSympRepo PatientSymp { get; }
        IPatBioRepo PatBio { get; }
        IPatRayRepo PatRay { get; }
        IPatAnalysisRepo PatAnalysis { get; }
        IPatMedRepo PatMed { get; }
        IPatCtRepo PatCt { get; }
        #endregion
        #region Service
        IOperationRepository<Diagnostic> Diagnositic { get; }
        IOperationRepository<Symptoms> Symptoms { get; }
        IOperationRepository<Biometrics> Biometric { get; }
        IOperationRepository<CT> CT { get; }
        IOperationRepository<Medicine> Medicine { get; }
        IOperationRepository<Analysis> Analysis { get; }
        IOperationRepository<Rays> Rays { get; }
        #endregion
        IUserDataForAppRepo UserData { get; }
        IWorkReop Work { get; }
        void Submit();
        Task SubmitAsync();

    }
}
