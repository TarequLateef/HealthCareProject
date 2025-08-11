using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatRays, Schema = HealthSchema.Patient)]
    public class PatientRays : GeneralRegister
    {
        [Key]
        public string PRID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatientID { get; set; }
        [Required, ForeignKey(ServTab.Rays)]
        public string RayID { get; set; }
        [Required, ForeignKey(PatientTab.Booking)]
        public string BookingID { get; set; }
        [StringLength(100)]
        public string? RayNote { get; set; }
        [StringLength(100)]
        public string? Descirption { get; set; }
        [StringLength(50)]
        public string? X_Lab { get; set; }
        public string CompID { get; set; }

        public PatientData ParientDataTBL { get; set; }
        public Rays RaysTBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
