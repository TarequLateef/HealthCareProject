using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class DiagnosticDTO : DtoRegister
    {
        private string _diagID = string.Empty;
        public string DiagID
        {
            get => string.IsNullOrEmpty(_diagID) ? Guid.NewGuid().ToString() : _diagID;
            set => _diagID=value;
        }
        public string DiagnosticName { get; set; }
        public string CompTypeID { get; set; }
    }

    public struct DiagFields
    {
        public const string Name = "DiagName";
    }
}
