using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories
{
    public class ToKhaiTrieuChungRepository : Repository<ToKhaiTrieuChung>, IToKhaiTrieuChungRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public ToKhaiTrieuChungRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public ICollection<ToKhaiTrieuChung> GetByToKhaiId(int toKhaiId)
        {
            return _context.ToKhaiTrieuChungs.Where(x => x.ToKhaiId == toKhaiId).ToList();
        }
    }
}
