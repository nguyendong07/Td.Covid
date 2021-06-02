using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model;

namespace TD.Covid.Data.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
        Area GetByCode(string code);
        ICollection<Area> GetByParentCode(string code);
        ICollection<Area> GetAreaLevel2();
        ICollection<Area> GetByCodes(string codes);
        Dictionary<string, string> GetCurrentArea();
    }
}
