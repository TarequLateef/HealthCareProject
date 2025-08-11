using GeneralMotabea.Core.General;
using System.Runtime.InteropServices;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatRayDTO : DtoGeneralRegister
    {
        private string _prID = string.Empty;
        public string PRID
        {
            get => string.IsNullOrEmpty(_prID) ? Guid.NewGuid().ToString() : _prID;
            set => _prID=value;
        }
        public string PatientID { get; set; }
        public string RayID { get; set; }
        public string? RayNote { get; set; }
        public string? Descirption { get; set; }
        public string? X_Lab { get; set; }
        public string BookingID { get; set; }
        public string CompID { get; set; }
    }

    public struct PatRayFields
    {
        public const string Name = "PatRayName";
        public const string Desc = "PatRayDesc";
        public const string Note = "PatRayNote";
        public const string Date = "PatRayDate";
        public const string Lab = "PatRayLab";
        public const string ParamPat = "&patRayID=";
    }
}
