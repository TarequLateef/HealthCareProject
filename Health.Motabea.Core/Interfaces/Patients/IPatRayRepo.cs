using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Models.Patients;

namespace Health.Motabea.Core.Interfaces.Patients
{
    public interface IPatRayRepo:IOperationRepository<PatientRays>,IAvailableRepository<PatientRays>
    {
    }
}
