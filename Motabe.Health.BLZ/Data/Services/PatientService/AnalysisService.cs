using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using Motabe.Health.BLZ.Data.Services.Patients;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class AnalysisService : MotabeService<Analysis, AnalysisDTO>
    {
        public AnalysisService()
        {
            //
            this.Item = new Analysis();
            this.OperationItem = new AnalysisDTO();
            //
            this.ControllerName="Analysis/";
            this.ListUrl="AllAnalysis";
            this.DetailsUrl="AnalysisDet";
            this.AddUrl="AddAnal";
            this.UpdateUrl="UpdateAnal";
            this.DeleteUrl="DelAnal";
            //
            this.ListWin="AnalList";
            this.AddWin="AddAnal";
            this.DetailsWin="AnalDet";
        }

        public IEnumerable<Analysis> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == AnalFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.AnalysisName == item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.AnalysisName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.AnalysisName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.AnalysisName != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == AnalFields.Measure)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.NormalMeasure== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.NormalMeasure.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.NormalMeasure.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.NormalMeasure != item.SearchValue).ToList();
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
