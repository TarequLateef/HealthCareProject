
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.DTOs.Patients;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.Booking, Schema = HealthSchema.Patient)]
    public class Booking : GeneralRegister
    {
        [Key]
        public string BookID { get; set; }
        [Required, ForeignKey(PatientTab.BaseData)]
        public string PatientID { get; set; }
        [Required]
        public int Ordering { get; set; }
        [Required]
        public bool Repeated { get; set; }
        [Required]
        public string CompID { get; set; }
        [Required]
        public bool EnsureBook { get; set; }
        [StringLength(20), Required, DefaultValue(typeof(string))]
        public string BookStatus { get; set; } = BookRep.EnClinic;

        public PatientBaseData PatientBaseTBL { get; set; }
    }
}
