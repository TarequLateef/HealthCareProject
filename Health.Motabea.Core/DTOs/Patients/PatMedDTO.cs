using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatMedDTO:DtoGeneralRegister
    {
        private string _patMedID = string.Empty;
        public string PatMedID
        {
            get => string.IsNullOrEmpty(_patMedID) ? Guid.NewGuid().ToString() : _patMedID;
            set => _patMedID=value;
        }
        public string PatID { get; set; }
        public string MedID { get; set; }
        public string BookID { get; set; }
        public string Dose { get; set; }
        public string Period { get; set; }
        public string? Note { get; set; }
        public string CompID { get; set; }
    }

    public class PatMedGroup
    {
        public DateTime MedDate { get; set; }
        public string VisitNo { get; set; }
        public int CountOfMed { get; set; }
    }
    public struct PatMedFields
    {
        public const string Name = "PatMedName";
        public const string Dose = "PatMedDose";
        public const string Period = "PatMedPer";
        public const string Note = "PatMedNote";
        public const string Date = "PatMedDate";
        public const string ContPat = "&patID=";
        public const string ParamDate = "&"+PatMedFields.Date+"=";
        public const string ParamPat = "&patMedID=";
    }

    public struct MedPeriods
    {
        public const string Hour = "Hour";
        public const string Day = "/Day";
    }
}
