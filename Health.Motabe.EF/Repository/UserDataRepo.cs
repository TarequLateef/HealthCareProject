using GeneralMotabea.Core.DTOs;
using GeneralMotabea.Core.Interfaces;
using Health.Motabea.EF;
using System.Net.Http.Json;

namespace Health.Motabe.EF.Repository
{
    public class UserDataRepo : OperationRepository<AutherData>, IUserDataForAppRepo
    {
        readonly HealthAppContext _ctx;
        HttpClient _http = new HttpClient();
        public UserDataRepo(HealthAppContext ctx) : base(ctx)
        {
            _ctx = ctx;
            _http.BaseAddress=new Uri("https://localhost:7061/api/");
            /*_http.BaseAddress=new Uri("http://169.254.88.71:7058/api/");*/
        }

        public async Task<AutherData> GetUserData(string lID)
        {
            string userData = _http.BaseAddress+ "login/GetCurrLog?lt=";
            AutherData auther= await _http.GetFromJsonAsync<AutherData>(userData+lID)??new AutherData();
            return auther;
        }
    }
}
