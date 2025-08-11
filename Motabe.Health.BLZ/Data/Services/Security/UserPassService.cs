using UserManagement.Core.DTOs.Security;
using UserManagement.Core.Models.Secureity;

namespace Motabe.Health.BLZ.Data.Services.Security
{
    public class UserPassService:MotabeService<UserPass,UserPassDTO>
    {
        public UserPassService():base(false)
        {
            //
            this.Item = new UserPass();
            this.OperationItem = new UserPassDTO();
            //
            this.ControllerName="UserPass/";
            this.ListUrl="UserPassList";
            this.DetailsUrl="UserPassDet";
            this.AddUrl="AddUserPass";
            this.UpdateUrl="UpdateUserPass";
            this.StopRestoreUrl="StopResUserPass";
            this.DeleteUrl="DelUP";
            //
            this.UpdateWin="UpdateUserPass";

        }
    }
}
