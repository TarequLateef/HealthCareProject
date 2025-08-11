using GeneralMotabea.Core.Interfaces;
using Health.Motabea.Core.Models.Patients;

namespace Health.Motabea.Core.Interfaces.Patients
{
    public interface IBookRepo:IOperationRepository<Booking>,IAvailableRepository<Booking>
    {
        Task<bool> Repeated(Booking _booking);
    }
}
