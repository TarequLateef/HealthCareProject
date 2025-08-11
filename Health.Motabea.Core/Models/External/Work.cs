using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Motabea.Core.Models.External
{
    [Table(CompanyTables.Work,Schema =SchemaNames.CompSchema)]
    public class Work:Register
    {
        [Key]
        public string WorkID { get; set; }
        [Required, StringLength(20)]
        public string WorkName { get; set; }
        [Required, StringLength(2)]
        public  string WorkCode { get; set; }
    }
}
