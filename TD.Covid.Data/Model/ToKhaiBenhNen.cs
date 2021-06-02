using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Model
{
    public class ToKhaiBenhNen
    {
        public int ID { get; set; }
        public int ToKhaiId { get; set; }
        public ToKhai ToKhai { get; set; }
        public int BenhNenId {get;set;}
        public BenhNen BenhNen { get; set; }
    }
}
