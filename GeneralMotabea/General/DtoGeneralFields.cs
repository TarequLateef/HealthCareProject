namespace GeneralMotabea.Core.General
{
    public class DtoGeneralRegister:DtoRegister
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public virtual DateTime? EndDate { get; set; }
    }

    public class DtoRegister
    {
        public string UserLogID { get; set; }
        public DateTime RegistTime { get; set; } = DateTime.Now;
    }

    public class DtoAvailable
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
    }
}
