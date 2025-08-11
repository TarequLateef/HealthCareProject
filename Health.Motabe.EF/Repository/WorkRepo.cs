using Health.Motabea.Core.Interfaces;
using Health.Motabea.EF;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.Http.Json;
using System.Security.Cryptography;
using UserManagement.Core.DTOs;
using UserManagement.Core.DTOs.Security;
using UserManagement.Core.Models.Address;
using UserManagement.Core.Models.Secureity;

namespace Health.Motabe.EF.Repository
{
    public class WorkRepo : OperationRepository<Work>, IWorkReop
    {
        readonly HealthAppContext _ctx;
        HttpClient _http = new HttpClient();
        public WorkRepo(HealthAppContext ctx) : base(ctx)
        {
            _ctx = ctx;
            _http.BaseAddress=new Uri("https://localhost:7058/api/");
        }

        public async Task<HttpResponseMessage> AddWork(WorkDTO workDTO)
        {
            string addWorkUrl = _http.BaseAddress+"Work/AddWork";
            return await _http.PostAsJsonAsync<WorkDTO>(addWorkUrl, workDTO);
            /*public async Task<HttpResponseMessage> AddItem(DTO item, string url) =>
            await _http.PostAsJsonAsync<DTO>(url, item);*/

        }

        public async Task<IList<Work>> AllWorks()
        {
            string getWorkURL = _http.BaseAddress+"Work/AllWorks";
            return await _http.GetFromJsonAsync<IList<Work>>(getWorkURL)??new List<Work>();
        }
    }
}
