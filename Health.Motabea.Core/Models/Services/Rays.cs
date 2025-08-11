using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Rays, Schema = HealthSchema.Serv)]
    public class Rays : Register
    {
        [Key]
        public string RayID { get; set; }
        [Required, StringLength(20)]
        public string RayName { get; set; }
        [StringLength(100)]
        public string? Descirption { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
