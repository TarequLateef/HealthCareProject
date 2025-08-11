using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class AnalysisDTO : DtoRegister
    {
        private string _analysis = string.Empty;
        public string AnalysisID
        {
            get => string.IsNullOrEmpty(_analysis) ? Guid.NewGuid().ToString() : _analysis;
            set => _analysis=value;
        }
        public string AnalysisName { get; set; }
        public string? NormalMeasure { get; set; }
        public string CompTypeID { get; set; }
    }

    public struct AnalFields
    {
        public const string Name = "AnalName";
        public const string Measure = "AnalMeasure";
    }
}
