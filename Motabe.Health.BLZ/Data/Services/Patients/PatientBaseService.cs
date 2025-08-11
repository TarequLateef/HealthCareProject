using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;
using GeneralMotabea.Core.General.DbStructs;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class PatientBaseService : MotabeService<PatientBaseData, PatientBaseDTO>
    {
        public string ByPhoneUrl => "PatientByPhone";
        public PatientBaseService()
        {
            this.Item = new PatientBaseData();
            this.OperationItem = new PatientBaseDTO();
            //
            ControllerName = "PatienData/";
            ListUrl = "AllPatients";
            DetailsUrl = "PatienDetails";
            AddUrl = "AddPatient";
            UpdateUrl = "UpdatePatient";
            DeleteUrl = "DeletePatient";
            //
            this.AddWin="NewPatBase";
            this.ListWin="PatBaseList";
            this.DetailsWin="PatBaseData";
        }

       public void ReplaceUpdateItem(PatientBaseData upItem, IList<PatientBaseData> patientList)
        {
            var item = patientList.First(i => i.PatientID==upItem.PatientID);
            int itemIndx = patientList.IndexOf(item);
            patientList[itemIndx]=upItem;
        }
         
        #region Phone
        ReturnState<PatientBaseData> _returnItem { get; set; } = new ReturnState<PatientBaseData>();

        public ReturnState<PatientBaseData> ReturnItem { get => _returnItem; set => _returnItem = value; }

        public async Task<ReturnState<PatientBaseData>> ExistPhone(string phone) =>
            await _http.GetFromJsonAsync<ReturnState<PatientBaseData>>(ControllerName + ByPhoneUrl + "?" + PatBaseFields.Phone + "=" + phone);

        #endregion

        public IEnumerable<PatientBaseData> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == PatBaseFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.PatientName == item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.PatientName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.PatientName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.PatientName != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == PatBaseFields.Phone)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.Phone == item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.Phone.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.Phone.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.Phone != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == PatDataFields.Code)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.PatientCode == item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.PatientCode.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.PatientCode.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.PatientCode != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

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
