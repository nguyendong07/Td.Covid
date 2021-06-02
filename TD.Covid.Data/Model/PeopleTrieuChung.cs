using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;

namespace TD.Covid.Data.Model
{
    public class PeopleTrieuChung
    {
        public int ID { get; set; }
        public int PeopleId { get; set; }
        public People People { get; set; }
        public int TrieuChungId { get; set; }
        public TrieuChung TrieuChung { get; set; }
    }
}
