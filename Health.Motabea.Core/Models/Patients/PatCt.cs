using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatCT, Schema = HealthSchema.Patient)]
    public class PatCt : GeneralRegister
    {
        [Key]
        public string PatCtID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required, ForeignKey(ServTab.CT)]
        public string CTID { get; set; }
        [Required,ForeignKey(PatientTab.Booking)]
        public string BookID { get; set; }
        [StringLength(100)]
        public string? CtResult { get; set; }
        public string CompID { get; set; }

        public PatientData ParientDataTBL { get; set; }
        public CT CT_TBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
