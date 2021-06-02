using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Covid.Data.FilterModel
{
    public class BaseFilterModel
    {
        public BaseFilterModel()
        {
            Skip = 0;
            Top = 100;
            Q = string.Empty;
            OrderBy = string.Empty;
            Count = false;
            Include = string.Empty;
            Active = null;
        }

        public BaseFilterModel(string skipStr, string topStr, string q, string orderBy, string countStr, string include, string activeStr)
        {
            Skip = !string.IsNullOrEmpty(skipStr) ? int.Parse(skipStr) : 0;
            Top = !string.IsNullOrEmpty(topStr) ? int.Parse(topStr) : 100;
            Q = q;
            OrderBy = orderBy;
            Count = !string.IsNullOrEmpty(countStr) && bool.Parse(countStr);
            Include = include;
            Active = !string.IsNullOrEmpty(activeStr) ? bool.Parse(activeStr) : (bool?)null;
        }

        public int Skip { get; set; }

        public int Top { get; set; }

        public string Q { get; set; }

        public string OrderBy { get; set; }

        public bool Count { get; set; }

        public string Include { get; set; }

        public bool? Active { get; set; }
    }
}
