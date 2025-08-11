
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatMed, Schema = HealthSchema.Patient)]
    public class PatMed : GeneralRegister
    {
        [Key]
        public string PatMedID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required, ForeignKey(ServTab.Medicine)]
        public string MedID { get; set; }
        [Required, ForeignKey(PatientTab.Booking)]
        public string BookID { get; set; }
        [Required, StringLength(20)]
        public string Dose { get; set; }
        [Required, StringLength(20)]
        public string Period { get; set; }
        [StringLength(50)]
        public string? Note { get; set; }
        public string CompID { get; set; }

        public PatientData ParientDataTBL { get; set; }
        public Medicine MedicineTBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
