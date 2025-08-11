using Microsoft.JSInterop;
using Motabe.Health.BLZ.Data.Interface;

namespace Motabe.Health.BLZ.Data
{
    public class CookieService : ICookieService
    {
        readonly IJSRuntime _jsRunTime;
        readonly IHttpContextAccessor _contextAccessor;
        string expires = string.Empty;
        public CookieService(IJSRuntime jSRuntime, IHttpContextAccessor contextAccessor)
        { _jsRunTime = jSRuntime; _contextAccessor = contextAccessor; }

        public async Task<string> GetCookie() =>
            await _jsRunTime.InvokeAsync<string>(CookieData.CookieName, $"document.cookie");

        public async Task SetCookie(string value) =>
             await _jsRunTime.InvokeVoidAsync(CookieData.CookieName, $"document.cookie=\"{value}\"");


        public async Task<string> GetValue(string key, string def = "")
        {
            var cValue = await GetCookie();
            if (string.IsNullOrEmpty(cValue)) return def;

            var vals = cValue.Split(';');
            foreach (var val in vals)
                if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                    if (val.Substring(0, val.IndexOf('=')).Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                        return val.Substring(val.IndexOf('=') + 1);

            return def;

        }

        public string? RequestGetCookie(string key) =>
            _contextAccessor.HttpContext is null ?
            string.Empty : _contextAccessor.HttpContext.Request.Cookies[key];


        public async Task SetValue(string key, string value, int? days = null)
        {
            var currExp = days is not null ? days > 0 ? DateToUTC(days.Value) : "" : expires;
            await SetCookie($"{key}={value}; expires={currExp}; path=/");
        }
        static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");

    }

    public struct CookieData
    {
        public const string CookieName = "eval";
        public const string CurrID = "ts";
    }
}
