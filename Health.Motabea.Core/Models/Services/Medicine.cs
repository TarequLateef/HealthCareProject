using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.Medicine, Schema = HealthSchema.Serv)]
    public class Medicine : Register
    {
        [Key]
        public string MedID { get; set; }
        [Required, StringLength(50)]
        public string MedName { get; set; }
        [Required, StringLength(15)]
        public string MedType { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
