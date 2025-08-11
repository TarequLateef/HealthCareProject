using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatDiagService:MotabeService<PatientDiag,PatientDiagDTO>
    {
        public PatDiagService()
        {
            this.Item = new PatientDiag();
            this.OperationItem = new PatientDiagDTO();
            //
            this.ControllerName="PatDiag/";
            this.ListUrl="AllPatDiags";
            this.DetailsUrl="PatDiagDet";
            this.AddUrl="AddPatDiag";
            this.UpdateUrl="UpdatePatDiag";
            this.StopRestoreUrl="RestorePatDiag";
            this.DeleteUrl="DeletePd";

        }
    }
}
