using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Patients
{
    [Table(PatientTab.BaseData,Schema = HealthSchema.Patient)]
    public class PatientBaseData:Register
    {
        [Key]
        public string PatientID { get; set; }
        [Required, StringLength(50)]
        public string PatientName { get; set; }
        [Required, StringLength(11)]
        public string Phone { get; set; }
        [Required]
        public string CompID { get; set; }
        [Required, StringLength(100)]
        public string CountryID { get; set; }
        [Required, StringLength(100)]
        public string GovernID { get; set; }
        [Required, StringLength(100)]
        public string CityID { get; set; }
        [Required]
        public DateTime AgeDate { get; set; }
        [StringLength(11)]
        public string? OtherPhone { get; set; }
        [Required, StringLength(5)]
        public string Gender { get; set; }
        [Required]
        public string WorkID { get; set; }
        [Required]
        public bool Occup { get; set; }
        [Required, StringLength(13)]
        public string PatientCode { get; set; }
    }
}
