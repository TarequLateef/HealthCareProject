using GeneralMotabea.Core.General;

namespace UserManagement.Core.DTOs
{
    public class WorkDTO:DtoRegister
    {
        string _workID = string.Empty;
        public string WorkID
        {
            get => string.IsNullOrEmpty(_workID) ? Guid.NewGuid().ToString() : _workID;
            set => _workID=value;
        }
        public string WorkCode { get; set; }
        public string WorkName { get; set; }
    }
}
