using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.PatientService
{
    public class BiometricService:MotabeService<Biometrics,BioDTO>
    {
        public BiometricService()
        {
            //
            this.Item = new Biometrics();
            this.OperationItem = new BioDTO();
            //
            this.ControllerName="Biometric/";
            this.ListUrl="AllBiometrics";
            this.DetailsUrl="BiometricsDet";
            this.AddUrl="AddBio";
            this.UpdateUrl="UpdateBio";
            this.DeleteUrl="DelBio";
            //
            this.ListWin="BioList";
            this.AddWin="AddBio";
            this.DetailsWin="BioDet";
        }
        public IEnumerable<Biometrics> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == BioFields.Name)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.BioName== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.BioName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.BioName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.BioName != item.SearchValue).ToList();
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
