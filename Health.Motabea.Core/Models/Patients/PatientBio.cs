using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatBio,Schema =HealthSchema.Patient)]
    public class PatientBio:GeneralRegister
    {
        [Key]
        public string PatBioID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required, ForeignKey(ServTab.Biometirc)]
        public string BioID { get; set; }
        [Required,ForeignKey(PatientTab.Booking)]
        public string BookID { get; set; }
        [StringLength(20)]
        public string? BioMeasure { get; set; }
        [StringLength(100)]
        public string? Note { get; set; }
        public string CompID { get; set; }
        public Biometrics BiometricTBL { get; set; }
        public PatientData ParientDataTBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
