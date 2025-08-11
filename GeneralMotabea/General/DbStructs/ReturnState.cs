namespace GeneralMotabea.Core.General.DbStructs
{
    public class ReturnState<T>
    {
        public string Message { get; set; }
        public T Item { get; set; }
        public bool Status { get; set; }
    }
}
