using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model
{
    public class Area : CategoryBase, ITrackableModel
    {
        public const int LEVEL_NATION = 1;
        public const int LEVEL_PROVINCE = 2;
        public const int LEVEL_DISTRICT = 3;
        public const int LEVEL_WARD = 4;

        public string Type { get; set; }

        public int? ParentId { get; set; }

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedAt { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
}
