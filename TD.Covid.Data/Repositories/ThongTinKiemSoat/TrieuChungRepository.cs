using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ThongTinKiemSoat
{
    public class TrieuChungRepository : Repository<TrieuChung>, ITrieuChungRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public TrieuChungRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }
    }
}
