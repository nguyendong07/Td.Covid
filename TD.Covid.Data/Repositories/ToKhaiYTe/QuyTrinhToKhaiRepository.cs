using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ToKhaiYTe
{
    public class QuyTrinhToKhaiRepository : Repository<QuyTrinhToKhai>, IQuyTrinhToKhaiRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public QuyTrinhToKhaiRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }
    }
}
