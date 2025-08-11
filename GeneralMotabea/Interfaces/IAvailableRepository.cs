
namespace GeneralMotabea.Core.Interfaces
{
    public interface IAvailableRepository<T> where T : class
    {
        Task<IList<T>> AvailableListAsync();
        Task<IList<T>> BannedListAsync();
        bool IsAvalaible(T Item);
        T RestorStop(T sItem);
        Task<T> RestoreStopAsync(T sItem);
    }
}
