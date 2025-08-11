using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Analysis,Schema =HealthSchema.Serv)  ]
    public class Analysis:Register
    {
        [Key]
        public string AnalysisID { get; set; }
        [Required,StringLength(50)]
        public string AnalysisName { get; set; }
        public string? NormalMeasure { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
