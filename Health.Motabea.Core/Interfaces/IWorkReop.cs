using GeneralMotabea.Core.Interfaces;
using UserManagement.Core.DTOs;
using UserManagement.Core.Models.Address;

namespace Health.Motabea.Core.Interfaces
{
    public interface IWorkReop:IOperationRepository<Work>
    {
        Task<IList<Work>> AllWorks();
        Task<HttpResponseMessage> AddWork(WorkDTO workDTO);
    }
}
