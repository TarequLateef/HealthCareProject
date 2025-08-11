using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class RaysService:MotabeService<Rays,RaysDTO>
    {
        public RaysService()
        {
            this.Item = new Rays();
            this.OperationItem = new RaysDTO();
            //
            this.ControllerName = "Rays/";
            this.ListUrl="AllRays";
            this.DetailsUrl="RaysDet";
            this.AddUrl="AddRays";
            this.UpdateUrl="UpdateRays";
            this.DeleteUrl="DelRays";
            //
            this.ListWin="RayList";
            this.AddWin="AddRay";
            this.DetailsWin="RayData";
        }

        public IEnumerable<Rays> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == RayFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.RayName== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.RayName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.RayName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.RayName != item.SearchValue).ToList();
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
