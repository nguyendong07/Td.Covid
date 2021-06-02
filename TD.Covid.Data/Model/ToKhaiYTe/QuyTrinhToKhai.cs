using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model.ToKhaiYTe
{
    public class QuyTrinhToKhai
    {
        public int ID { get; set; }
        public int ToKhaiID { get; set; }
        public ToKhai ToKhai { get; set; }
        public string Name { get; set; }
        public string NguoiKiemSoatID { get; set; }
        public string NguoiKiemSoatName { get; set; }
    }
}
