using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TD.Covid.Data.Model.BanDoDiaDiem;
using TD.Covid.Data.Model.ThongBao;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.ViewModels;
using TD.Core.Api.Mvc;

namespace TD.Covid.Api.Controllers.ThongBao
{
    public class CovidPatientController : TDApiController
    {
        private ICovidPatientRepository _repository;
        public CovidPatientController(ICovidPatientRepository repository)
        {
            _repository = repository;
        }

        [Route("~/covidapi/covidpatient")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var entities = _repository.GetAll();
            return ApiOk(entities);
        }

        [Route("~/covidapi/covidpatient")]
        [HttpPost]
        public IHttpActionResult Post(CovidPatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            var entity = _repository.CreateCovidPatient(request);
            return ApiCreated(entity);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/covidpatient/{id:int:min(1)}")]
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
        [Route("~/covidapi/covidpatient/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, CovidPatient change)
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
        [Route("~/covidapi/covidpatient/{id:int:min(1)}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, CovidPatient change)
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
        [Route("~/covidapi/covidpatient/{id:int:min(1)}")]
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
        [Route("~/covidapi/covidpatient/count")]
        [HttpGet]
        public IHttpActionResult Count()
        {
            var count = _repository.Count();
            return ApiOk(count);
        }
    }
}
