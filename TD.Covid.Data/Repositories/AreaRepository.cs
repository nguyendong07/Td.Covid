using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model;
using TD.Core.Api.Mvc;
using Microsoft.SharePoint;
using TD.Core.UserProfiles.Controllers;
using TD.Core.UserProfiles.Models;
namespace TD.Covid.Data.Repositories
{
    public class AreaRepository: Repository<Area>, IAreaRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public AreaRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public ICollection<Area> GetAreaLevel2()
        {
            return _context.Areas.Where(x => x.Type == "2").ToList();
        }

        public Area GetByCode(string code)
        {
            return _context.Areas.FirstOrDefault(x => x.Code == code);
        }

        public ICollection<Area> GetByCodes(string codes)
        {
            var codeList = codes.Split(',');

            var result = new List<Area>();

            foreach (var code in codeList)
            {
                var tmp = _context.Areas.FirstOrDefault(x => x.Code == code);
                result.Add(tmp);
            }


            return result;
        }

        public ICollection<Area> GetByParentCode(string code)
        {
            var query = _context.Areas.AsQueryable();
            if (code != "")
            {
                var _idParent = query.FirstOrDefault(x => x.Code == code);
                query = query.Where(x => x.ParentId == _idParent.Id);
            }
            return query.ToList();
        }

        public Dictionary<string,string> GetCurrentArea()
        {
            Dictionary<string, string> ob = new Dictionary<string, string>();
            string areaCode = string.Empty;
            string districtCode = string.Empty;
            string provinceCode = string.Empty;
            string urlRoot = SPContext.Current.Site.RootWeb.Url;
            using (SPSite oSite = new SPSite(urlRoot))
            {
                var webApp = oSite.WebApplication;
                var zone = oSite.Zone;

                UserProfileController userProfileCtrlr = new UserProfileController(webApp, zone);
                UserProfile obj = userProfileCtrlr.GetByCurrentUser();
                areaCode = obj.AreaCode;
            }
            ob.Add("areacode", areaCode);
            switch (areaCode.Length)
            {
                case 2:
                    ob.Add("provinceCode", areaCode);
                    ob.Add("districtCode", "");
                    ob.Add("wardCode", "");
                    break;
                case 3:
                case 4:
                    if(Int32.TryParse(GetByCode(areaCode).ParentId.ToString(),out int result))
                    {
                        provinceCode = GetById(result).Code;
                    }
                    ob.Add("provinceCode", provinceCode);
                    ob.Add("districtCode", areaCode);
                    ob.Add("wardCode", "");
                    break;
                case 5:
                    if (Int32.TryParse(GetByCode(areaCode).ParentId.ToString(), out int rs))
                    {
                        districtCode = GetById(rs).Code;
                        if (Int32.TryParse(GetByCode(districtCode).ParentId.ToString(), out int r))
                        {
                            provinceCode = GetById(r).Code;
                        }
                    }  
                    ob.Add("provinceCode", provinceCode);
                    ob.Add("districtCode", districtCode);
                    ob.Add("wardCode", areaCode);
                    break;
            }
            return ob;
        }
    }
}
