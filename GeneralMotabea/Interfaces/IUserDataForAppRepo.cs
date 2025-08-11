using GeneralMotabea.Core.DTOs;

namespace GeneralMotabea.Core.Interfaces
{
    public interface IUserDataForAppRepo : IOperationRepository<AutherData>
    {
        Task<AutherData> GetUserData(string lID);
    }
}
