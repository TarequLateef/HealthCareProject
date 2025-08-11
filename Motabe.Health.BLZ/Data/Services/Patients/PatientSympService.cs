using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatientSympService:MotabeService<PatientSymptom, PatSympDTO>
    {
        public PatientSympService()
        {
            this.Item = new PatientSymptom();
            this.OperationItem = new PatSympDTO();
            //
            this.ControllerName = "PatientSymp/";
            this.ListUrl= "AllPatSympo";
            this.DetailsUrl = "PatSympDet";
            this.AddUrl = "AddPatSymp";
            this.UpdateUrl = "UpdatePatSymp";
            this.StopRestoreUrl="RestStopPatSymp";
            this.DeleteUrl = "DelPatSymp";
            // 
            this.AddWin = "NewPatSymp";
            this.ListWin = "PatSympList";
            this.DetailsWin = "PatSympDet";
        }
    }
}
