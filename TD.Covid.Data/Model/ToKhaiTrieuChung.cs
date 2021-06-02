using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Model
{
    public class ToKhaiTrieuChung
    {
        public int ID { get; set; }
        public int ToKhaiId {get;set;}
        public ToKhai ToKhai { get; set; }
        public int TrieuChungId { get; set; }
        public TrieuChung TrieuChung { get; set; }
    }
}
