using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatRayService:MotabeService<PatientRays,PatRayDTO>
    {
        public PatRayService()
        {
            this.Item = new PatientRays();
            this.OperationItem = new PatRayDTO();
            //
            this.ControllerName="PatRay/";
            this.ListUrl="AllPatRays";
            this.DetailsUrl="PatRayDet";
            this.AddUrl="AddPatRay";
            this.UpdateUrl="UpdatePatRay";
            this.StopRestoreUrl="StopPatRay";
            this.DeleteUrl="DelPatRay";

        }
    }
}
