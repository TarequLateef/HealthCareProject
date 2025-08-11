using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class MedicinDTO : DtoRegister
    {
        private string _mID = string.Empty;
        public string MedID
        {
            get => string.IsNullOrEmpty(_mID) ? Guid.NewGuid().ToString() : _mID;
            set => _mID=value;
        }
        public string MedName { get; set; }
        public string MedType { get; set; }
        public string CompTypeID { get; set; }

    }

    public struct MedFields
    {
        public const string Name = "MedName";
        public const string Type = "MedType";
    }

    public struct MedTypes
    {
        public const string Caps = "Cabs";
        public const string Soup = "Soup";
        public const string Inj = "Injection";
        public const string Inh = "Inhaler";
    }
}
