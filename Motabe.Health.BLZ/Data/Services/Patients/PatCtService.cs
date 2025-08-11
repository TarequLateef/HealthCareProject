using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatCtService:MotabeService<PatCt,PatCtDTO>
    {
        public PatCtService()
        {
            this.Item = new PatCt();
            this.OperationItem = new PatCtDTO();
            //
            this.ControllerName="PatCt/";
            this.ListUrl="AllPatCt";
            this.DetailsUrl="PatCtDet";
            this.AddUrl="AddPatCt";
            this.UpdateUrl="UpdatePatCt";
            this.StopRestoreUrl="StopPatCt";
            this.DeleteUrl="DelPatCt";

        }
    }
}
