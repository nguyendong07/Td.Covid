using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model.ThongBao
{
    public class VungBiNhiem
    {
        public int ID { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int WardCode { get; set; }
        public int Active { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ProvinceText { get; set; }
        public string  DistrictText { get; set; }
        public int WardTextpublic { get; set; }
}
}
