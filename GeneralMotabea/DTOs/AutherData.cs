
namespace GeneralMotabea.Core.DTOs
{
    public class AutherData
    {
        public string UserLoginID { get; set; }
        public string UserID { get; set; }
        public bool IsAuthenticated =>
            !(string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(UserLoginID));
        public IList<string> Roles { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string CompID { get; set; }
        public string CompTypeID { get; set; }
        public IList<AppDataDto> UserAppList { get; set; }
    }

    public class AppDataDto
    {
        public string AppID { get; set; }
        public string AppName { get; set; }
        public string AppLink { get; set; }

    }
}
