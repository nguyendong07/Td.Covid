using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.Model.BanDoDiaDiem
{
    public class PatientFtLocation
    {
        public int ID { get; set; }
        public int CovidPatientId { get; set; }
        public CovidPatient CovidPatient { get; set; }
        public int LocationId {get; set;}
        public Location Location { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
    }
}
