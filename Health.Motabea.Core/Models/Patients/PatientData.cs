using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.Patient, Schema = HealthSchema.Patient)]
    public class PatientData : Register
    {
        [Key, ForeignKey(PatientTab.BaseData)]
        public string PatientID { get; set; }
        [Required]
        public bool Smoker { get; set; }
        [Required]
        public bool ExSmoker { get; set; }
        [Required,StringLength(20)]
        public bool PassiveSmoker { get; set; }
        [Required]
        public bool ContactToBird { get; set; }
        public bool Pregmant { get; set; }
        public bool Lactating { get; set; }

        public PatientBaseData PatientBaseTBL { get; set; }
    }
}
