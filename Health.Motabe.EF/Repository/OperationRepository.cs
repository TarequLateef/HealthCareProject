using GeneralMotabea.Core.Interfaces;
using Health.Motabea.EF;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UserManagement.Core.Models.Goods;

namespace Health.Motabe.EF.Repository
{
    public class OperationRepository<T> : IOperationRepository<T> where T : class
    {
        readonly HealthAppContext _Hctx;
        public OperationRepository(HealthAppContext healthAppContext) => _Hctx=healthAppContext;
        #region Operation
        public async Task<T> AddItem(T item)
        {
            await _Hctx.Set<T>().AddAsync(item);
            return item;
        }

        public int Count(Expression<Func<T, bool>> criteria) => _Hctx.Set<T>().Count(criteria);

        public void Delete(T item) => _Hctx.Set<T>().Remove(item);

        public async Task<IList<T>> Delete(int id)
        {
            T item = await _Hctx.Set<T>().FindAsync(id);
            if(item is not null) _Hctx.Set<T>().Remove(item);
            return _Hctx.Set<T>().ToList();
        }

        public async Task<IList<T>> Delete(string id)
        {
            T item = await _Hctx.Set<T>().FindAsync(id);
            if (item is not null) _Hctx.Set<T>().Remove(item);
            return _Hctx.Set<T>().ToList();

        }
        public async Task<IList<T>> GetAll() => await _Hctx.Set<T>().ToArrayAsync();

        public async Task<T> GetByIntID(int id) => await _Hctx.Set<T>().FindAsync(id);

        public async Task<T> GetByStringID(string id) => await _Hctx.Set<T>().FindAsync(id);

        public bool IsDeletable(Expression<Func<T, bool>> criteria) =>
            !_Hctx.Set<T>().Any(criteria);

        public bool IsExists(Expression<Func<T, bool>> criteria, out T item)
        {
            item =_Hctx.Set<T>().Find(criteria);
            return item is not null;
        }

        public bool IsExists(Expression<Func<T, bool>> criteria) => _Hctx.Set<T>().Any(criteria);

        public async Task<T> Update(int id, T item)
        {
            var updateItem = await _Hctx.Set<T>().FindAsync(id);
            _Hctx.Entry(updateItem).CurrentValues.SetValues(item);
            return updateItem;

        }

        public async Task<T> Update(string id, T item)
        {
            var updateItem = await _Hctx.Set<T>().FindAsync(id);
            _Hctx.Entry(updateItem).CurrentValues.SetValues(item);
            return updateItem;
        }
        public T Update(T item)
        {
            _Hctx.Update(item);
            return item;

        }
        #endregion
        #region Search
        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria) =>
            await _Hctx.Set<T>().Where(criteria).ToArrayAsync();

        public async Task<IList<T>> Find(string[] subTables)
        {
            IQueryable<T> list = _Hctx.Set<T>();
            foreach (var tbl in subTables) list = list.Include(tbl);
            return await list.ToArrayAsync();
        }

        public async Task<IList<T>> Find(int take, int skip) =>
            await _Hctx.Set<T>().Take(take).Skip(skip).ToArrayAsync();

        public async Task<IList<T>> Find(Expression<Func<T, object>> OrderByField, string OrderingField = "Acending")
        {
            IQueryable<T> list = _Hctx.Set<T>();
            return OrderingField==Ordering.Asc ?
                await list.OrderBy(OrderByField).ToArrayAsync()
                : await list.OrderByDescending(OrderByField).ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, Expression<Func<T, object>> OrderByField, string OrderingField = "Acending")
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            foreach (var tbl in subTables) list=list.Include(tbl);
            return OrderingField==Ordering.Asc ?
                await list.OrderBy(OrderByField).ToArrayAsync()
                : await list.OrderByDescending(OrderByField).ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = "Acending")
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            list = list.Take(take).Skip(skip);
            return OrderingField==Ordering.Asc ?
                await list.OrderBy(OrderByField).ToArrayAsync()
                : await list.OrderByDescending(OrderByField).ToArrayAsync();
        }

        public async Task<IList<T>> Find(string[] subTables, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = "Acending")
        {
            IQueryable<T> list = _Hctx.Set<T>();
            list = list.Take(take).Skip(skip);
            foreach (var tbl in subTables) list = list.Include(tbl);
            return OrderingField==Ordering.Asc ?
                            await list.OrderBy(OrderByField).ToArrayAsync()
                            : await list.OrderByDescending(OrderByField).ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, int take, int skip, Expression<Func<T, object>> OrderByField, string OrderingField = "Acending")
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            list = list.Take(take).Skip(skip);
            foreach (var tbl in subTables) list = list.Include(tbl);
            return OrderingField==Ordering.Asc ?
                            await list.OrderBy(OrderByField).ToArrayAsync()
                            : await list.OrderByDescending(OrderByField).ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables)
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            foreach (var tbl in subTables) list = list.Include(tbl);
            return await list.ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            return await list.Take(take).Skip(skip).ToArrayAsync();
        }

        public async Task<IList<T>> Find(string[] subTables, int take, int skip)
        {
            IQueryable<T> list = _Hctx.Set<T>();
            return await list.Take(take).Skip(skip).ToArrayAsync();
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> criteria, string[] subTables, int take, int skip)
        {
            IQueryable<T> list = _Hctx.Set<T>().Where(criteria);
            list = list.Take(take).Skip(skip);
            foreach (var tbl in subTables) list=list.Include(tbl);
            return await list.ToArrayAsync();
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> criteria) =>
                await _Hctx.Set<T>().FirstOrDefaultAsync(criteria);
            

        public async Task<T> FindSingle(Expression<Func<T, bool>> criteria, string[] subTables)
        {
            IQueryable<T> val = _Hctx.Set<T>();
            foreach (var tbl in subTables) val = val.Include(tbl);
            return await val.FirstOrDefaultAsync(criteria);

        }
        #endregion
        
    }
}
