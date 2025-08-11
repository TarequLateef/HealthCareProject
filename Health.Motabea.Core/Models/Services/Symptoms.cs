using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Symptoms,Schema =HealthSchema.Serv)]
    public class Symptoms:Register
    {
        [Key]
        public string SymptomID { get; set; }
        [Required,StringLength(30)]
        public string SymptonName { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
