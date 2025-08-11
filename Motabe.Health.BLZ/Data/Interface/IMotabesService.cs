
namespace Motabe.Health.BLZ.Data.Interface
{
    public interface IMotabesService<T,DTO>
    {
        #region Events
        /*public bool  ExistUsers { get; }*/
        /*public bool SaveNotification { get; }
        public bool ItemSaved { get; }*/
        IList<T> ShowRange(IList<T> list, int currPage);
        Task<IList<T>> GetDataList(string actionUrl);
        Task<IList<T>> GetDataList(string actionUrl, bool aval);
        Task<T> GetData(string actionUrl, string itemID);
        Task<T> GetData(string actionUrl);
        Task<HttpResponseMessage> AddItem(DTO item, string url);
        Task<HttpResponseMessage> UptateItem(DTO item, string url, string id);
        Task<HttpResponseMessage> StopRestoreItem(DTO item, string url, string id);
        Task DeleteItem(string url, int id);
        Task DeleteItem(string url, string id);
        #endregion
    }
}
