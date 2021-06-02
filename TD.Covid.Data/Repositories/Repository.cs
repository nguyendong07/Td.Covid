using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TD.Covid.Data.FilterModel;
using TD.Covid.Data.Model;
using TD.Core.Api.Mvc;
using Z.Expressions;

namespace TD.Covid.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;

        public Repository(DbContext context, ICoreContextAccessor coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public virtual T Add(T model)
        {
            if (model is ITrackableModel trackable)
            {
                trackable.CreatedAt = DateTime.Now;
            }

            _context.Set<T>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            _context.SaveChanges();
        }

        public virtual ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> IncludeMany(IQueryable<T> queryable, string include)
        {
            ICollection<string> includeCollection = null;
            if (!string.IsNullOrEmpty(include))
            {
                includeCollection = new Regex(@"\s*,\s*").Split(include);
            }

            if (includeCollection != null && includeCollection.Count > 0)
            {
                foreach (var item in includeCollection)
                {
                    queryable = queryable.Include(item);
                }
            }
            return queryable;
        }

        public virtual IQueryable<T> OrderByMany(IQueryable<T> queryable, string orderBy)
        {
            var splitChars = new char[] { '|' };

            ICollection<string> orderByCollection = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                orderByCollection = new Regex(@"\s*,\s*").Split(orderBy);
            }

            if (orderByCollection == null)
            {
                return queryable.OrderByDynamic(x => $"x.Id");
            }
            var checkOrdered = false;
            foreach (string str in orderByCollection)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }

                var spl = str.Split(splitChars);

                var field = spl[0];
                var desc = spl.Length > 1 && spl[1].ToUpper() == "DESC";

                if (!checkOrdered)
                {
                    if (desc)
                    {
                        queryable = queryable.OrderByDescendingDynamic(x => $"x.{field}");
                    }
                    else
                    {
                        queryable = queryable.OrderByDynamic(x => $"x.{field}");
                    }
                    checkOrdered = true;
                }
                else
                {
                    if (desc)
                    {
                        queryable = ((IOrderedQueryable<T>)queryable).ThenByDescendingDynamic(x => $"x.{field}");
                    }
                    else
                    {
                        queryable = ((IOrderedQueryable<T>)queryable).ThenByDynamic(x => $"x.{field}");
                    }
                }

            }

            return queryable;
        }

        public virtual T Update(T model)
        {
            _context.Set<T>().Attach(model);

            if (model is ITrackableModel trackable)
            {
                trackable.ModifiedAt = DateTime.Now;
            }

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return model;
        }

        /// <summary>
        /// Handle include, orderby, q, active
        /// </summary>
        public virtual IQueryable<T> CreateQuery<U>(U filterModel) where U : BaseFilterModel
        {
            var query = _context.Set<T>().AsQueryable();

            // include
            query = IncludeMany(query, filterModel.Include);

            // order by
            query = OrderByMany(query, filterModel.OrderBy);

            // FIXME : process q

            // active
            if (query is IQueryable<CategoryBase> query1 && filterModel.Active != null)
            {
                query = (IQueryable<T>)query1.Where(x => x.Active == filterModel.Active);
            }

            return query;
        }

        public virtual T UpdateChanges(T model)
        {
            //Ensure only modified fields are updated.
            _context.Set<T>().Attach(model);
            foreach (var property in _context.Entry(model).OriginalValues.PropertyNames)
            {
                var original = _context.Entry(model).OriginalValues.GetValue<object>(property);
                var current = _context.Entry(model).CurrentValues.GetValue<object>(property);
                if (current != null && !string.IsNullOrEmpty(current.ToString()) && !current.Equals(original))
                    _context.Entry(model).Property(property).IsModified = true;
            }

            if (model is ITrackableModel trackable)
            {
                trackable.ModifiedAt = DateTime.Now;
            }

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return model;
        }
    }
}
