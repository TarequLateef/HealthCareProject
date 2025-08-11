using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatAnalService:MotabeService<PatAnalysis,PatAnalysDTO>
    {
        public PatAnalService()
        {
            this.Item=new PatAnalysis();
            this.OperationItem=new PatAnalysDTO();
            //
            this.ControllerName="PatAnalysis/";
            this.ListUrl="AllPatAnalysis";
            this.DetailsUrl="PartAnalDet";
            this.AddUrl="AddPatAnal";
            this.UpdateUrl="UpdatePatAnal";
            this.StopRestoreUrl="StopPatAnal";
            this.DeleteUrl="DelPatAnal";

        }
    }
}
