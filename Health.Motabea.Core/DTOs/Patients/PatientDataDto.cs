using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatientDataDto : DtoRegister
    {
        private string _patientID = string.Empty;
        public string PatientID
        {
            get => string.IsNullOrEmpty(_patientID) ? Guid.NewGuid().ToString() : _patientID;
            set => _patientID=value;
        }
        public bool Smoker { get; set; }
        public bool ExSmoker { get; set; }
        public bool PassiveSmoker { get; set; }
        public bool ContactToBird { get; set; }
        public bool Pregmant { get; set; }
        public bool Lactating { get; set; }
    }

    public struct Genders
    {
        public const string EnMale = "Male";
        public const string ArMale = "ذكر";
        public const string EnFmale = "Fmale";
        public const string ArFmale = "انثى";
        public string[] EnGenderList = { EnMale, EnFmale };
        public string[] ArGenderList = { ArMale, ArFmale };
        public Genders() { }
    }
    public struct PatDataFields
    {
        public const string Code = "PatCode";
        public const string Age = "PatAge";
        public const string Gender = "PatGen";
        public const string Work = "PatWork";
        public const string Occup = "PatOcc";
        public const string Smoker = "PatSmoker";
        public const string ExSmoker = "PatExSmoke";
        public const string Passive = "PatPassSmoke";
    }

    public struct AddressFields
    {
        public const string Country = "PatCountry";
        public const string Govern = "PatGovern";
        public const string City = "PatCity";
    }
}
