using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class MedicineService : MotabeService<Medicine, MedicinDTO>
    {
        public MedicineService()
        {
            this.Item = new Medicine();
            this.OperationItem=new MedicinDTO();
            //
            this.ControllerName="Medicine/";
            this.ListUrl="AllMedicine";
            this.DetailsUrl="MedicineDet";
            this.AddUrl="AddMedicine";
            this.UpdateUrl="UpdateMedicine";
            this.DeleteUrl="DelMedicine";
            //
            this.ListWin="MedList";
            this.AddWin="NewMed";
            this.DetailsWin="MedData";
        }

        public IEnumerable<Medicine> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == MedFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.MedName== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.MedName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.MedName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.MedName != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == MedFields.Type)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.MedType== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.MedType.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.MedType.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.MedType!= item.SearchValue).ToList();
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
