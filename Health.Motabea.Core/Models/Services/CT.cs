using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.Services
{
    [Table(ServTab.CT, Schema = HealthSchema.Serv)]
    public class CT : Register
    {
        [Key]
        public string CT_ID { get; set; }
        [Required, StringLength(50)]
        public string CT_Name { get; set; }
        [Required]
        public string CompTypeID { get; set; }
    }
}
