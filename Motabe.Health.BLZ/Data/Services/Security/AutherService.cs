using GeneralMotabea.Core.DTOs;
using Motabe.Health.BLZ.Data.Interface;
using UserManagement.Core.DTOs.Security;

namespace Motabe.Health.BLZ.Data.Services.Security
{
    public class AutherService:MotabeService<AutherData,AutherData>
    {
        ICookieService _cookie;
        public AutherService(ICookieService cookie):base(false)
        {
            _cookie=cookie;
            //
            this.Item  = new AutherData();
            this.OperationItem = new AutherData();
            //
            this.ControllerName="Login/";
            this.DetailsUrl = "GetCurrLog";
        }

        public async Task<AutherData> ReadLogData()
        {
            string lID = _cookie.RequestGetCookie(CookieData.CurrID);
            this.Item = string.IsNullOrEmpty(lID) ? new AutherData()
               : await this.GetData(this.DetailsUrl+"&lt="+lID);
            return this.Item;
        }

    }
}
