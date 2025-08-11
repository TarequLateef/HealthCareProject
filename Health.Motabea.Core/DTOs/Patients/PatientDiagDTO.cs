using GeneralMotabea.Core.General;

namespace Health.Motabea.Core.DTOs.Patients
{
    public class PatientDiagDTO : DtoGeneralRegister
    {
        private string _patDiagID = string.Empty;
        public string PatDiagID
        {
            get => string.IsNullOrEmpty(_patDiagID) ? Guid.NewGuid().ToString() : _patDiagID;
            set => _patDiagID=value;
        }
        public string DiagID { get; set; }
        public string PatID { get; set; }
        public bool PrimaryDiag { get; set; }
        public string BookingID { get; set; }
        public string CompID { get; set; }
    }
}
