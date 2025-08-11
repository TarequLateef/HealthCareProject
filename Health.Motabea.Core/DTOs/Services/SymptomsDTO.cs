using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class SymptomsDTO : DtoRegister
    {
        private string _sympID = string.Empty;
        public string SymptomID
        {
            get => string.IsNullOrEmpty(_sympID) ? Guid.NewGuid().ToString() : _sympID;
            set => _sympID=value;
        }
        public string SymptonName { get; set; }
        public string CompTypeID { get; set; }
    }

    public struct SympFields
    {
        public const string Name = "SympName";
    }
}
