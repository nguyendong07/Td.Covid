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
    public class TinhTrangTheoDoisController : TDApiController
    {
        private ITinhTrangTheoDoiRepository _repository;
        public TinhTrangTheoDoisController(ITinhTrangTheoDoiRepository repository)
        {
            _repository = repository;
        }

        [Route("~/covidapi/tinhtrangtheodois")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var entities = _repository.GetAll();
            return ApiOk(entities);
        }

        [Route("~/covidapi/tinhtrangtheodois")]
        [HttpPost]
        public IHttpActionResult Post(TinhTrangTheoDoi model)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            var entity = _repository.Add(model);
            return ApiCreated(entity);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/tinhtrangtheodois/{id:int:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var model = _repository.GetById(id);

            if (model == null)
            {
                return ApiNotFound();
            }

            return ApiOk(model);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/tinhtrangtheodois/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, TinhTrangTheoDoi change)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            if (id != change.ID)
            {
                return ApiBadRequest();
            }

            _repository.Update(change);
            return ApiNoContent();
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/tinhtrangtheodois/{id:int:min(1)}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, TinhTrangTheoDoi change)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            if (id != change.ID)
            {
                return ApiBadRequest();
            }

            _repository.UpdateChanges(change);
            return ApiNoContent();
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/tinhtrangtheodois/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var model = _repository.GetById(id);

            if (model == null)
            {
                return ApiNotFound();
            }

            _repository.Delete(model);
            return ApiNoContent();
        }

        [Route("count")]
        [Route("~/covidapi/tinhtrangtheodois/count")]
        [HttpGet]
        public IHttpActionResult Count()
        {
            var count = _repository.Count();
            return ApiOk(count);
        }
    }
}
