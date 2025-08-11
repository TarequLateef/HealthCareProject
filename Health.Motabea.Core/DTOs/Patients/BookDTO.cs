using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class BookDTO : DtoGeneralRegister
    {
        private string _bookID = string.Empty;
        public string BookID
        {
            get => string.IsNullOrEmpty(_bookID) ? Guid.NewGuid().ToString() : _bookID;
            set => _bookID=value;
        }
        public string PatientID { get; set; }
        public int Ordering { get; set; }
        public bool Repeated { get; set; }
        public string CompID { get; set; }
        public bool EnsureBook { get; set; }
        public string BookStatus { get; set; } 
    }

    public struct BookRep
    {
        public const string ArClinic = "كشف";
        public const string ArRep = "استشارة";
        public const string ArAnalysis = "تحليل";
        public const string ArXRay = "اشاعة";
        public const string ArTreat = "علاج";

        public const string EnClinic = "Exam";
        public const string EnRep = "Consult";
        public const string EnAnalysis = "Analysis";
        public const string EnXRay = "X-ray";
        public const string EnTreat = "Treatment";

        public static string EnStatus(string st)
        {
            string stu = string.Empty;
            switch (st)
            {
                case EnClinic:
                case ArClinic:
                    stu=EnClinic; break;
                case EnRep:
                case ArRep:
                    stu=EnRep; break;
                case EnAnalysis:
                case ArAnalysis:
                    stu=EnAnalysis; break;
                case EnXRay:
                case ArXRay:
                    stu=EnXRay; break;
                case EnTreat:
                case ArTreat:
                    stu=EnTreat; break;
                default: stu=EnClinic; break;
            }
            return stu;
        }
        public static string ArStatus(string st)
        {
            string stu = string.Empty;
            switch (st)
            {
                case EnClinic:
                case ArClinic:
                    stu=ArClinic; break;
                case EnRep:
                case ArRep:
                    stu=ArRep; break;
                case EnAnalysis:
                case ArAnalysis:
                    stu=ArAnalysis; break;
                case EnXRay:
                case ArXRay:
                    stu=ArXRay; break;
                case EnTreat:
                case ArTreat:
                    stu=ArTreat; break;
                default: stu=EnClinic; break;
            }
            return stu;
        }
    }
}
