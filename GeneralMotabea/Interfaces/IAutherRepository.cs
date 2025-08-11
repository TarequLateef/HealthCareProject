using GeneralMotabea.Core.Models.UserManagment;

namespace GeneralMotabea.Core.Interfaces
{
    public interface IAutherRepository
    {
        Task<AutherModel> RegisterAsync(RegisterModel regist);
        Task<ApplicationUser> ExistMailAsync(string email);
        Task<AutherModel> GetTokenAsync(TokenRequestModel token);
        Task<string> AddRoleAsync(AddRoleModel role);
        Task<AutherModel> RefreshToken(string token);
        Task<bool> RevokeTokenAsync(string token);

    }
}
