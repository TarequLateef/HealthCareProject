
namespace Motabe.Health.BLZ.Data.Interface
{
    public interface ICookieService
    {
        Task SetValue(string key, string value, int? days = null);
        Task<string> GetValue(string key, string def = "");
        /*bool CookieExists(CookieCollection cookie, string key);
        void RemoveCookie(string key);*/
        string RequestGetCookie(string key);
        Task<string> GetCookie();
        Task SetCookie(string value);
    }
}
