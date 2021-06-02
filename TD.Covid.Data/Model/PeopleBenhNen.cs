using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;

namespace TD.Covid.Data.Model
{
    public class PeopleBenhNen
    {
        public int ID { get; set; }
        public int PeopleId { get; set; }
        public People People { get; set; }
        public int BenhNenId { get; set; }
        public BenhNen BenhNen { get; set; }
    }
}
