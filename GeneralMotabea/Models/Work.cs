using GeneralMotabea.Core.General;
using GeneralMotabea.Core.General.DbStructs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Core.Models.Address
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
