using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model.ThongBao
{
    public class ThongBaoKhanCap
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int WardCode { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string NoiDung { get; set; }
    }
}
