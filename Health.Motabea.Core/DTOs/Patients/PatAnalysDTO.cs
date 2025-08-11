using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatAnalysDTO : DtoGeneralRegister
    {
        private string _patAnalID = string.Empty;
        public string PatAnalID
        {
            get => string.IsNullOrEmpty(_patAnalID) ? Guid.NewGuid().ToString() : _patAnalID;
            set => _patAnalID=value;
        }
        public string PatID { get; set; }
        public string AnalysisID { get; set; }
        public string? AnalResult { get; set; }
        public string? LabName { get; set; }
        public string BookID { get; set; }
        public string CompID { get; set; }
    }

    public struct PatAnalFields
    {
        public const string Anal = "PatAnal";
        public const string Result = "PatResult";
        public const string Date = "PatDate";
        public const string Lab = "PatLab";
        public const string PatParam = "&patAnalID=";
    }
}
