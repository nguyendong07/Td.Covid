using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.FilterModel;

namespace TD.Covid.Data.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Add(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Update(T model);

        /// <summary>
        /// Update only modified properties
        /// FIXME: it update default properties
        /// </summary>
        /// <param name="model"></param>
        T UpdateChanges(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryable<T> IncludeMany(IQueryable<T> query, string include);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<T> OrderByMany(IQueryable<T> query, string orderBy);

        /// <summary>
        /// Create query from BaseFilterModel
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        IQueryable<T> CreateQuery<U>(U filterModel) where U : BaseFilterModel;
    }
}
