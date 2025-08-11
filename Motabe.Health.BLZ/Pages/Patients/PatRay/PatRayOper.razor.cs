using AutoMapper;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using HSB_COMP.BLZ.Comps;
using Microsoft.AspNetCore.Components;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

namespace Motabe.Health.BLZ.Pages.Patients.PatRay
{
    public partial class PatRayOper
    {
        [Inject] AutherService _authServ { get; set; }
        [Inject] RaysService _rayServ { get; set; }
        [Inject] PatRayService _pRayServ { get; set; }
        [Inject] IMapper rayMap { get; set; }

        [Parameter] public PatientRays PatRayItem { get; set; }
        [Parameter] public EventCallback<PatientRays> EvPatRay { get; set; }
        [Parameter] public EventCallback<IList<ErrorStatus>> EvPatRayErr { get; set; }
        [Parameter] public Booking BookItem { get; set; }
        bool showAdd = false;

        protected override async Task OnInitializedAsync()
        {
            //
            _pRayServ.CurrUserID=_authServ.Item.UserLoginID;
            //
            PatRayItem.UserLogID=_authServ.Item.UserLoginID;
            PatRayItem.CompID=_authServ.Item.CompID;
            PatRayItem.BookingID= BookItem.BookID;
            PatRayItem.PatientID=BookItem.PatientID;
            //
            await this.LoadRayList(new Rays());
            this.SetPatDate(BookItem.StartDate.ToString("yyyy-MM-dd"));
        }

        async Task LoadRayList(Rays anal)
        {
            _rayServ.OperationList = await _rayServ.GetDataList(_rayServ.ListUrl);
            _rayServ.DataList=
                _rayServ.OperationList.Where(b => b.CompTypeID==_authServ.Item.CompTypeID).ToList();
        }

        void SelectRay(string bioID)
        {
            PatRayItem.RayID=bioID;
            this.Invoke();
        }

        void SelectDesc(string mName)
        {
            PatRayItem.Descirption=mName;
            this.Invoke();
        }

        void SelectNote(string rNote)
        {
            PatRayItem.RayNote=rNote;
            this.Invoke();
        }

        void SetPatDate(string dt)
        {
            PatRayItem.StartDate=Convert.ToDateTime(dt);
            this.Invoke();
        }

        void SetPatLab(string lab)
        {
            PatRayItem.X_Lab=lab;
            this.Invoke();
        }
        void Invoke()
        {
            EvPatRay.InvokeAsync(PatRayItem);
            EvPatRayErr.InvokeAsync(_pRayServ.ErrorList);
        }
    }
}
