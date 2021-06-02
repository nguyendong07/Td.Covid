using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TD.Covid.Data.ViewModels;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD.Covid.Data.Model.BanDoDiaDiem
{
    public class Location 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Describer { get; set; }

        [NotMapped]
        public Area Province { get; set; }
        public int ProvinceCode { get; set; }
        [NotMapped]
        public Area District { get; set; }
        public string DistrictCode { get; set; }

        [NotMapped]
        public Area Ward { get; set; }
        public string WardCode { get; set; }
        
        public string Address { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        
        public int LocationTypeId { get; set; }
        public LocationType LocationType { get; set; }
    }
}
