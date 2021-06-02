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
    public class QuocTichRepository : Repository<QuocTich>, IQuocTichRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public QuocTichRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public ICollection<QuocTich> GetByName(string name)
        {
            return _context.QuocTichs.Where(x => x.Name == name).ToList();
        }
    }
}
