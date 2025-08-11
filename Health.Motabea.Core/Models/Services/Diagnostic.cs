
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Diagnostic,Schema =HealthSchema.Serv)]
    public class Diagnostic:Register
    {
        [Key]
        public string DiagID { get; set; }
        [Required, StringLength(80)]
        public string DiagnosticName { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
