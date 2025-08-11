using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Models.Patients;
using System.Globalization;

namespace Health.Motabea.Core.Interfaces.Patients
{
    public interface IPatMedRepo:IOperationRepository<PatMed>,IAvailableRepository<PatMed>
    {
        Task<IList<PatMed>> SpecPatMed(string patID, DateTime PatMedDate);
    }
}
