using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class BioDTO : DtoRegister
    {
        private string _bioID = string.Empty;
        public string BioID
        {
            get => string.IsNullOrEmpty(_bioID) ? Guid.NewGuid().ToString() : _bioID;
            set => _bioID=value;
        }
        public string BioName { get; set; }
        public string CompTypeID { get; set; }
        public string? NormalMeasure { get; set; }

    }

    public struct BioFields
    {
        public const string Name = "BioName";
        public const string Measure = "BioMeasure";
    }
}
