using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Core.General;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.PatAnalysis, Schema = HealthSchema.Patient)]
    public class PatAnalysis : GeneralRegister
    {
        [Key]
        public string PatAnalID { get; set; }
        [Required, ForeignKey(PatientTab.Patient)]
        public string PatID { get; set; }
        [Required, ForeignKey(ServTab.Analysis)]
        public string AnalysisID { get; set; }
        [Required,ForeignKey(PatientTab.Booking)]
        public string BookID { get; set; }
        [StringLength(100)]
        public string? AnalResult { get; set; }
        [StringLength(50)]
        public string? LabName { get; set; }
        public string CompID { get; set; }

        public PatientData ParientDataTBL { get; set; }
        public Analysis AnalysisTBL { get; set; }
        public Booking BookingTBL { get; set; }
    }
}
