
using System.Linq.Expressions;

namespace GeneralMotabea.Core.Interfaces
{
    public interface IOperationRepository<T> where T : class
    {
        #region Base Operation
        Task<T> GetByIntID(int id);
        Task<T> GetByStringID(string id);
        Task<IList<T>> GetAll();
        Task<T> AddItem(T item);
        Task<T> Update(int id, T item);
        Task<T> Update(string id, T item);
        T Update(T item);
        void Delete(T item);
        Task<IList<T>> Delete(int id);
        Task<IList<T>> Delete(string id);
        int Count(Expression<Func<T, bool>> criteria);
        bool IsExists(Expression<Func<T, bool>> criteria, out T item);
        bool IsExists(Expression<Func<T, bool>> criteria);
        bool IsDeletable(Expression<Func<T, bool>> criteria);

        #endregion

        #region Searching Single
        Task<T> FindSingle(Expression<Func<T, bool>> criteria);
        Task<T> FindSingle(Expression<Func<T, bool>> criteria, string[] subTables);
        #endregion
      
        #region Searching List
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria);
        Task<IList<T>> Find(string[] subTables);
        Task<IList<T>> Find(int take, int skip);
        Task<IList<T>> Find(Expression<Func<T, object>> OrderByField, string OrderingField = Ordering.Asc);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, Expression<Func<T, object>> OrderByField, string OrderingField = Ordering.Asc);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = Ordering.Asc);
        Task<IList<T>> Find(string[] subTables, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = Ordering.Asc);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = Ordering.Asc);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, int take, int skip);
        Task<IList<T>> Find(string[] subTables, int take, int skip);
        Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, int take, int skip);
        #endregion

    }

    public struct Ordering
    {
        public const string Asc = "Acending";
        public const string Desc = "Descending";
    }

}
