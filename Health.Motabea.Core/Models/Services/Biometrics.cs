using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Biometirc, Schema = HealthSchema.Serv)]
    public class Biometrics : Register
    {
        [Key]
        public string BioID { get; set; }
        [Required, StringLength(50)]
        public string BioName { get; set; }
        [Required]
        public string CompTypeID { get; set; }
        [StringLength(20)]
        public string? NormalMeasure { get; set; }
    }
}
