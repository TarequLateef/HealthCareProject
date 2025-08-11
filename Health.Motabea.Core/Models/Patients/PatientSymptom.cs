using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatientSymp, Schema = HealthSchema.Patient)]
    public class PatientSymptom : GeneralRegister
    {
        [Key]
        public string PatSympID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required, ForeignKey(ServTab.Symptoms)]
        public string SympID { get; set; }
        [Required, ForeignKey(PatientTab.Booking)]
        public string BookID { get; set; }
        [StringLength(6)]
        public string SympStatus { get; set; }
        [StringLength(120)]
        public string? Description { get; set; }
        [Required]
        public string CompID { get; set; }

        public PatientData ParientDataTBL { get; set; }
        public Symptoms SymptomsTBL { get; set; }
        public Booking BookingTBL { get; set; }

    }
    public struct SympLevel
    {
        public const string Hard = "Hard";
        public const string Low = "Low";
        public const string Harder = "Harder";
        public const string Lower = "Lower";
        public string[] FirstSymp = { };
        public string[] RepeatSymp = {};
        public SympLevel()
        {
            this.FirstSymp=new string[] { SympLevel.Hard, SympLevel.Low };
            this.RepeatSymp=new string[] { SympLevel.Harder, SympLevel.Lower };
        }
    }


}
