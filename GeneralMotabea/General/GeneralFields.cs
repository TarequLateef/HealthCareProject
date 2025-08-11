
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace GeneralMotabea.Core.General
{
    public class AvailableData
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DefaultValue(typeof(DateTime?))]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public bool IsAvailable => !EndDate.HasValue || (EndDate.HasValue && EndDate.Value>DateTime.Now);
        public bool PeriodAvailable => StartDate<DateTime.Now && (EndDate.HasValue && EndDate.Value>DateTime.Now);
        public string? OldID { get; set; }
    }

    public class Register
    {
        [Required]
        public string UserLogID { get; set; }
        [Required, DataType(DataType.DateTime), DefaultValue(typeof(DateTime?))]
        public DateTime RegistTime { get; set; } = DateTime.Now;

    }


    public class GeneralRegister : Register
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DefaultValue(typeof(DateTime?))]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public virtual DateTime? EndDate { get; set; }

        public bool Available => !EndDate.HasValue || (EndDate.HasValue && EndDate.Value>DateTime.Now);
        public bool PeriodAvailable => StartDate<DateTime.Now && (EndDate.HasValue && EndDate.Value>DateTime.Now);

    }

}
