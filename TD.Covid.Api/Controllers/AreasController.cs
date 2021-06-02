using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Repositories;
using TD.Core.Api.Mvc;

namespace TD.Covid.Api.Controllers
{
    public class AreasController : TDApiController
    {
        private IAreaRepository _repository;
        public AreasController(IAreaRepository repository)
        {
            _repository = repository;
        }

        [Route("~/covidapi/areas")]
        [HttpGet]
        public IHttpActionResult GetAll(string parentCode)
        {
            var entities = _repository.GetByParentCode(parentCode);
            return ApiOk(entities);
        }

        [Route("~/covidapi/areas/codes/{codes}")]
        [HttpGet]
        public IHttpActionResult GetByCodes(string codes)
        {
            var entities = _repository.GetByCodes(codes);
            return ApiOk(entities);
        }

        [Route("~/covidapi/areas/currentuser")]
        [HttpGet]
        public IHttpActionResult GetByCurrentUser()
        {
            var entities = _repository.GetCurrentArea();
            return ApiOk(entities);
        }
    }
}
