using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;

namespace TD.Covid.Data.Model.ThongTinLuuTru
{
    public class LichSuLuuTru
    {
        public int ID { get; set; }
        public int PeopleID { get; set; }
        public People People { get; set; }
        public int CoSoLuuTruID { get; set; }
        public CoSoLuuTru CoSoLuuTru { get; set; }
        public int CheckInDate { get; set; }
        public int CheckOutDate { get; set; }
    }
}
