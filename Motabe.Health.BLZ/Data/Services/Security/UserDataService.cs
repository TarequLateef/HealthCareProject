using Motabe.Health.BLZ.Data.Interface;
using UserManagement.Core.DTOs.Security;
using UserManagement.Core.Models.Secureity;

namespace Motabe.Health.BLZ.Data.Services.Security
{
    public class UserDataService : MotabeService<UserData, UserDataDTO>
    {
        public UserDataService() : base(false)
        {
            //
            this.Item = new UserData();
            this.OperationItem = new UserDataDTO();
            //
            this.ControllerName="UserData/";
            this.AddUrl="AddUser";
            this.ListUrl="AllUsers";
            this.DetailsUrl="UserDetails";
            this.UpdateUrl="UpdateUser";
            this.StopRestoreUrl="StopUser";
            this.DeleteUrl="DelUser";
            //
            this.ListWin="UserDataList";
            this.AddWin="RegistUser";
            this.DetailsWin="UserDet";
        }
    }
}
