using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class SypoService:MotabeService<Symptoms,SymptomsDTO>
    {
        public SypoService()
        {
            this.Item = new Symptoms();
            this.OperationItem = new SymptomsDTO();
            //
            this.ControllerName="Sympton/";
            this.ListUrl="AllSymptons";
            this.DetailsUrl="SymptonDet";
            this.AddUrl="AddSymp";
            this.UpdateUrl="UpdateSymp";
            this.DeleteUrl="DelSymp";
            //
            this.ListWin = "SympList";
            this.AddWin = "AddSymp";
            this.DetailsWin = "SympData";
        }
    }
}
