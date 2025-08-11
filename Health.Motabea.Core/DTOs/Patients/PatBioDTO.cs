using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatBioDTO : DtoGeneralRegister
    {
        private string _patBioID = string.Empty;
        public string PatBioID
        {
            get => string.IsNullOrEmpty(_patBioID ) ? Guid.NewGuid().ToString() : _patBioID ;
            set => _patBioID =value;
        }
        public string PatID { get; set; }
        public string BioID { get; set; }
        public string? BioMeasure { get; set; }
        public string? Note { get; set; }
        public string BookID { get; set; }
        public string CompID { get; set; }
    }

    public struct PatBioFields
    {
        public const string Bio = "patBio";
        public const string Date = "patBioDate";
        public const string PatBioParam = "&patID=";
    }
}
