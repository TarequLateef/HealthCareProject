using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatMedService:MotabeService<PatMed,PatMedDTO>
    {
        public string SpecPatMed { get; private set; }
        public PatMedService()
        {
            this.Item = new PatMed();
            this.OperationItem = new PatMedDTO();
            //
            this.ControllerName="PatMedicine/";
            this.ListUrl="AllPatMed";
            this.DetailsUrl="PatMedDet";
            this.AddUrl="AddPatMed";
            this.UpdateUrl="UpdatePatMed";
            this.StopRestoreUrl="StopPatMed";
            this.DeleteUrl="DelPatMed";

            this.SpecPatMed=this.ControllerName+"PatMedList?userID="+this.CurrUserID;
        }
    }
}
