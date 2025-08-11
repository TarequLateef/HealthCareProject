
using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatientBaseDTO : DtoRegister
    {
        private string _patientID = string.Empty;
        public string PatientID
        {
            get => string.IsNullOrEmpty(_patientID) ? Guid.NewGuid().ToString() : _patientID;
            set => _patientID=value;
        }
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public string CompID { get; set; }
        public string CountryID { get; set; }
        public string GovernID { get; set; }
        public string CityID { get; set; }
        public DateTime AgeDate { get; set; }
        public string? OtherPhone { get; set; }
        public string Gender { get; set; }
        public string WorkID { get; set; }
        public bool Occup { get; set; }
        public string PatientCode { get; set; }

    }

    public struct PatBaseFields
    {
        public const string Name = "Pname";
        public const string Phone = "Pphone";
        public const string ParamPID = "?pb=";
        public const string ParamBookDate = "&pd=";
    }

    public struct PatBaseTitle
    {
        public const string ArName = "الاسم";
        public const string EnName = "Name";
        public const string ArPhone = "تليفون";
        public const string EnPhone = "Phone";
        public const string ArGov = "المحافظة";
        public const string EnGov = "Government";
        public const string ArCity = "المدينة";
        public const string EnCity = "City";
        public const string ArAge = "السن";
        public const string EnAge = "Age";
        public const string ArWork = "الوظيفة";
        public const string EnWork = "Work";
        public const string ArGender = "النوع";
        public const string EnGender = "Gender";
        public const string ArOccup = "قائم على العمل";
        public const string EnOccup = "Occupation";
    }
}
