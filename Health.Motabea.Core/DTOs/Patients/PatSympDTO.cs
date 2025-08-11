using GeneralMotabea.Core.General;
using System.Net.Http.Headers;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatSympDTO : DtoGeneralRegister
    {
        private string _patsympID = string.Empty;
        public string PatSympID
        {
            get => string.IsNullOrEmpty(_patsympID) ? Guid.NewGuid().ToString() : _patsympID;
            set => _patsympID=value;
        }
        public string PatID { get; set; }
        public string SympID { get; set; }
        public string BookID { get; set; }
        public string SympStatus { get; set; }
        public string CompID { get; set; }
        public string? Description { get; set; }
    }

    public struct PatSysmpFields
    {
        public const string Patient = "PatSymp";
        public const string Sympo = "Sympo";
        public const string Status = "SypStatus";
        public const string Date = "SypDate";
        public const string PatParam = "&" + PatSysmpFields.Patient+"=";
    }
}
