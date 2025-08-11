using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class DiagService:MotabeService<Diagnostic,DiagnosticDTO>
    {
        public DiagService()
        {
            this.Item = new Diagnostic();
            this.OperationItem = new DiagnosticDTO();
            //
            this.ControllerName="Diag/";
            this.ListUrl="AllDiagnostic";
            this.DetailsUrl="DiagnosticDet";
            this.AddUrl="AddDiagnostic";
            this.UpdateUrl="UpdateDiagnostic";
            this.DeleteUrl="DelDiagnostic";
            //
            this.ListWin="DiagList";
            this.AddWin="AddDiag";
            this.DetailsWin="DiagData";
        }
        public IEnumerable<Diagnostic> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == DiagFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.DiagnosticName== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.DiagnosticName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.DiagnosticName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.DiagnosticName != item.SearchValue).ToList();
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
