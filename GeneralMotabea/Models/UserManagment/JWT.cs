namespace GeneralMotabea.Core.Models.UserManagment
{
    public class UserJWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpireTime { get; set; }

    }
}
