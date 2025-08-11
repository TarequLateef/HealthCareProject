using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatientDiag, Schema = HealthSchema.Patient)]
    public class PatientDiag : GeneralRegister
    {
        [Key]
        public string PatDiagID { get; set; }
        [Required]
        [ForeignKey(ServTab.Diagnostic)]
        public string DiagID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required]
        public bool PrimaryDiag { get; set; }
        [Required, ForeignKey(PatientTab.Booking)]
        public string BookingID { get; set; }
        [Required]
        public string CompID { get; set; }

        public Diagnostic DiagnosticTBL { get; set; }
        public PatientData ParientDataTBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
