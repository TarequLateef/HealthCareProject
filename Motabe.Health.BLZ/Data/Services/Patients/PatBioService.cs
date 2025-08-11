using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatBioService:MotabeService<PatientBio,PatBioDTO>
    {
        public PatBioService()
        {
            this.Item = new PatientBio();
            this.OperationItem = new PatBioDTO();
            //
            this.ControllerName="PatBio/";
            this.ListUrl="AllPatientBios";
            this.DetailsUrl="PatientBioDetails";
            this.AddUrl="AddPatBio";
            this.UpdateUrl="UpdatePatBio";
            this.StopRestoreUrl="StopPatBio";
            this.DeleteUrl="DelPatBio";
            //
            
        }
    }
}
