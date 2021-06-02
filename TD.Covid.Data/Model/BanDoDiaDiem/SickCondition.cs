﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TD.Covid.Data.ViewModels;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD.Covid.Data.Model.BanDoDiaDiem
{
    public class SickCondition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }

    }
}
