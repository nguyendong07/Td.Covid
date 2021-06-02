using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model;

namespace TD.Covid.Data.Repositories
{
    public interface IToKhaiTrieuChungRepository : IRepository<ToKhaiTrieuChung>
    {
        ICollection<ToKhaiTrieuChung> GetByToKhaiId(int toKhaiId);
    }
}
