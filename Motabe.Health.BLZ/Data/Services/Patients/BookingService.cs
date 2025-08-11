using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data.Services.Patients
{
    public class BookingService:MotabeService<Booking,BookDTO>
    {
        public BookingService()
        {
            this.Item = new Booking();
            this.OperationItem = new BookDTO();
            //
            this.ControllerName="Booking/";
            this.ListUrl="AllBooking";
            this.DetailsUrl="BookData";
            this.AddUrl="AddBook";
            this.UpdateUrl="UpdateBook";
            this.DeleteUrl="DeleteBook";
            this.StopRestoreUrl="StopBook";
            //
            this.AddWin="BookTicket";
            this.DetailsWin="BookData";
            this.ListWin="BookList";
        }

        public IEnumerable<Booking> SearchByField()
        {
            DataList = OperationList;
            Searching = false;
            foreach (var item in SearchArr)
            {
                if (item.FieldName == BookFields.Patient)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.PatientBaseTBL.PatientName == item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.PatientBaseTBL.PatientName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.PatientBaseTBL.PatientName.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.PatientBaseTBL.PatientName != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == BookFields.Phone)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.PatientBaseTBL.Phone== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.PatientBaseTBL.Phone.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.PatientBaseTBL.Phone.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.PatientBaseTBL.Phone != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == BookFields.Code)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.PatientBaseTBL.PatientCode== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.PatientBaseTBL.PatientCode.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.PatientBaseTBL.PatientCode.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.PatientBaseTBL.PatientCode != item.SearchValue).ToList();
                            break;
                        default:
                            DataList = DataList;
                            break;
                    }

                if (item.FieldName == BookFields.Status)
                    switch (item.Condition)
                    {
                        case Condition.Equal:
                            DataList = DataList.Where(e => e.BookStatus== item.SearchValue).ToList();
                            break;
                        case Condition.Contains:
                            DataList =
                                DataList.Where(e => e.BookStatus.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotContain:
                            DataList = DataList.Where(e => !e.BookStatus.Contains(item.SearchValue)).ToList();
                            break;
                        case Condition.NotEqual:
                            DataList = DataList.Where(e => e.BookStatus != item.SearchValue).ToList();
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
    public struct BookFields
    {
        public const string Patient = "BPat";
        public const string Date = "BDate";
        public const string Phone = "BPhone";
        public const string Code = "BookPatCode";
        public const string Status = "BState";
        public const string DateParam = "&BDate=";
    }

    public struct BookRepList
    {
        public string[] EnList =
            { BookRep.EnClinic, BookRep.EnRep ,BookRep.EnAnalysis,BookRep.EnXRay,BookRep.EnTreat};
        public string[] ArList =
            { BookRep.ArClinic, BookRep.ArRep, BookRep.ArAnalysis, BookRep.ArXRay, BookRep.ArTreat };
        public BookRepList() { }
    }

}
