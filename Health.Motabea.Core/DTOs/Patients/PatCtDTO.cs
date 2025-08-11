using AutoMapper.Execution;
using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatCtDTO : DtoGeneralRegister
    {
        private string _patCtID = string.Empty;
        public string PatCtID
        {
            get => string.IsNullOrEmpty(_patCtID) ? Guid.NewGuid().ToString() : _patCtID;
            set => _patCtID=value;
        }
        public string PatID { get; set; }
        public string CTID { get; set; }
        public string? CtResult { get; set; }
        public string CompID { get; set; }
        public string BookID { get; set; }
    }

    public struct PatCtFields
    {
        public const string Name = "PatCtName";
        public const string Result = "PatCtRes";
        public const string ParamPat = "&patCtID=";
    }
}
