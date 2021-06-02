using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model.ThongTinKiemSoat
{
    public class PhuongTien
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int LoaiPhuongTien { get; set; }
        public string BienSo { get; set; }
        public int SoGhe { get; set; }
    }
}
