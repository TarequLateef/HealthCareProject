using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Services
{
    public class RaysDTO:DtoRegister
    {
        private string _rayID = string.Empty;
        public string RayID
        {
            get => string.IsNullOrEmpty(_rayID) ? Guid.NewGuid().ToString() : _rayID;
            set => _rayID=value;
        }
        public string RayName { get; set; }
        public string CompTypeID { get; set; }
    }

    public struct RayFields
    {
        public const string Name = "RayName";
        public const string Desc = "RayDesc";
    }
}
