using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TD.Core.Api.Mvc;
using TD.Core.UserManagement.Extensions;
using TD.Core.Utilities.AspNet;
using TD.Core.PermissionHelpers;

namespace TD.Covid.Data.Repositories
{
    public class CoreContextAcessor : ICoreContextAccessor
    {
        // private readonly ILogger _logger;

        public CoreContextAcessor(/*ILogger logger*/)
        {
            // _logger = logger;
        }

        private HttpContext _httpContext;
        private Guid _siteId;
        private string _uposId;

        public string UserPositionCode
        {
            get
            {
                if (_uposId == null)
                {
                    try
                    {
                        _uposId = WebPermissionHelper.GetCurrentUserPositionId();
                    }
                    catch (Exception ex)
                    {
                        // _logger.Error(ex, "Error getting current user position");
                    }
                }

                return _uposId;
            }
        }

        public Guid ModuleSiteId
        {
            get
            {
                if (_siteId == null || _siteId == Guid.Empty)
                {
                    // get http context
                    var ctx = GetHttpContext();
                    if (ctx == null) return Guid.Empty;

                    // try get from header
                    var header = ctx.Request.Headers["TD-Site-Id"];
                    if (!string.IsNullOrEmpty(header))
                    {
                        _siteId = new Guid(header);
                        return _siteId;
                    }

                    // try get from query string
                    var qrStr = ctx.Request.QueryString.Get("siteid");
                    if (!string.IsNullOrEmpty(qrStr))
                    {
                        _siteId = new Guid(qrStr);
                        return _siteId;
                    }

                    // try get from fixed module name
                    var moduleController = ContextItems.Current.ModuleController;
                    var currentModule = moduleController.GetModuleByCode(Consts.ModuleCode);

                    if (currentModule != null && !string.IsNullOrWhiteSpace(currentModule.SiteId))
                    {
                        return new Guid(currentModule.SiteId);
                    }

                    //
                    var spCtx = SPContext.Current;
                    if (spCtx != null)
                    {
                        _siteId = spCtx.Site.ID;
                    }
                }

                return _siteId;
            }
        }

        private HttpContext GetHttpContext()
        {
            if (_httpContext == null)
            {
                _httpContext = HttpContext.Current;
                //if (_httpContext != null)
                //{
                //    HttpContext.Current = _httpContext;
                //}
            }

            return _httpContext;
        }
    }
}
