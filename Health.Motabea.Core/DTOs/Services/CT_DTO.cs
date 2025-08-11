using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class CT_DTO : DtoRegister
    {
        private string _ctID = string.Empty;
        public string CT_ID
        {
            get => string.IsNullOrEmpty(_ctID) ? Guid.NewGuid().ToString() : _ctID;
            set => _ctID=value;
        }
        public string CT_Name { get; set; }
        public string CompTypeID { get; set; }

    }

    public struct CtFields
    {
        public const string Name = "CtName";
    }
}
