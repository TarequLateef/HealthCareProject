using GeneralMotabea.Core.General.DbStructs;
using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Models.Patients;

namespace Health.Motabea.Core.Interfaces.Patients
{
    public interface IPatientDataRepo:IOperationRepository<PatientBaseData>
    {
        Task<ReturnState<PatientBaseData>> ByPhone(string phone);
        ReturnState<PatientBaseData> ErrorState();

    }
}
