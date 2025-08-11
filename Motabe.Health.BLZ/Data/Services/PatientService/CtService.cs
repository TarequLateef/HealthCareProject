using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class CtService:MotabeService<CT,CT_DTO>
    {
        public CtService()
        {
            this.Item = new CT();
            this.OperationItem = new CT_DTO();
            //
            this.ControllerName="CT/";
            this.ListUrl="AllCT";
            this.DetailsUrl="CTDet";
            this.AddUrl="AddCT";
            this.UpdateUrl="UpdateCT";
            this.DeleteUrl="DelCT";
            //
            this.ListWin="CtList";
            this.AddWin="AddCT";
            this.DetailsWin="CtData";
        }
        public IEnumerable<CT> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == BioFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.CT_Name== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.CT_Name.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.CT_Name.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.CT_Name != item.SearchValue).ToList();
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
