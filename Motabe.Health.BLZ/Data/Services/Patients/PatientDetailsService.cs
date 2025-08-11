using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatientDetailsService:MotabeService<PatientData,PatientDataDto>
    {
        public PatientDetailsService()
        {
            this.Item = new PatientData();
            this.OperationItem = new PatientDataDto();
            //
            this.ControllerName="PatientDetails/";
            this.ListUrl="AllPatients";
            this.DetailsUrl="PatientDatas";
            this.AddUrl="AddPatient";
            this.UpdateUrl="EditPatient";
            this.DeleteUrl="DelPat";
            //
            this.AddWin="SetPatientData";
            this.DetailsWin="PatientDataDet";
        }
        public IEnumerable<PatientData> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {

                Searching = !string.IsNullOrEmpty(item.SearchValue);
            }

            SearchList = DataList;
            DataList = ShowRange(SearchList, 1);

            return DataList;
        }

        public void SearchField(SearchTable st)
        {
            ManageSearchArr(st);
            SearchByField();
        }

    }
}
