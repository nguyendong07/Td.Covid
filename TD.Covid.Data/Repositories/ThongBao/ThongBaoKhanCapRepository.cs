﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongBao;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ThongBao
{
    public class ThongBaoKhanCapRepository : Repository<ThongBaoKhanCap>, IThongBaoKhanCapRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public ThongBaoKhanCapRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base (context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }
    }
}
