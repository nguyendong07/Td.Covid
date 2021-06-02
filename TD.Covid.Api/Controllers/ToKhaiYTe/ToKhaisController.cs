using System;
using System.Web.Http;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using TD.Covid.Data.Repositories.ToKhaiYTe;
using TD.Covid.Data.Services;
using TD.Covid.Data.ViewModels;
using TD.Core.Api.Mvc;
namespace TD.Covid.Api.Controllers.ToKhaiYTe
{
    public class ToKhaisController : TDApiController
    {
        private IToKhaiRepository _repository;
        private readonly AreaRepository _areaRepository;
        private readonly ChotKiemSoatRepository _chotKiemSoatRepository;
        private readonly INotificationService _notificationService;
        public ToKhaisController(IToKhaiRepository repository, AreaRepository areaRepository,
            ChotKiemSoatRepository chotKiemSoatRepository, INotificationService notificationService)
        {
            _repository = repository;
            _areaRepository = areaRepository;
            _notificationService = notificationService;
            _chotKiemSoatRepository = chotKiemSoatRepository;
        }
        
        [Route("~/covidapi/tokhais")]
        [HttpGet]
        public IHttpActionResult GetAll(int? trangThaiToKhaiID = 0, bool? isCurrent = null, int? chotKiemSoatID = 0)
        {
            //chotKiemSoatID = _chotKiemSoatRepository.GetChotKiemSoatTheoNguoiDung("");
            //var entities = _repository.Get(trangThaiToKhaiID, isCurrent, chotKiemSoatID);
            //return ApiOk(entities);
            return ApiOk();
        }

        [Route("~/covidapi/tokhais")]
        [HttpGet]
        public IHttpActionResult GetAll(int skip, int top, string areaCode)
        {
            if (String.IsNullOrEmpty(areaCode))
            {
                areaCode = _repository.GetAreaCodeByCurrentUser();
            }
            var entities = _repository.GetAllBase(null, null, null, null, null, null, null, null, null, null, skip.ToString(), top.ToString(), null, null, null, null, null, null, areaCode, "","","");
            return ApiOk(entities);
        }
        
        [Route("~/covidapi/tokhais")]
        [HttpPost]
        public IHttpActionResult Post(ToKhai model)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            model.TrangThaiToKhaiID = 2;

            var entity = _repository.Add(model);

            var provinceTo = _areaRepository.GetByCode(entity.ProvinceCodeTo);
            var districtTo = _areaRepository.GetByCode(entity.DistrictCodeTo);
            var wardTo = _areaRepository.GetByCode(entity.WardCodeTo);

            var proviceToName = provinceTo == null ? "" : provinceTo.Name;
            var districtToName = districtTo == null ? "" : districtTo.Name;
            var wardToName = wardTo == null ? "" : wardTo.Name;

            _notificationService.SendNotification(entity, proviceToName, districtToName, wardToName);

            return ApiCreated(entity);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/covidapi/tokhais/{id:int:min(1)}")]
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
        [Route("~/covidapi/tokhais/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult Put(int id, ToKhai change)
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
        [Route("~/covidapi/tokhais/{id:int:min(1)}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, ToKhai change)
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
        [Route("~/covidapi/tokhais/{id:int:min(1)}")]
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
        [Route("~/covidapi/tokhais/count")]
        [HttpGet]
        public IHttpActionResult Count()
        {
            var count = _repository.Count();
            return ApiOk(count);
        }

        [Route("statistic")]
        [Route("~/covidapi/tokhais/statistic")]
        [HttpGet]
        public IHttpActionResult Statistic()
        {
            var result = new
            {
                Pending = _repository.Count(2, true, 0),
                Approved = _repository.Count(3, true, 0),
                Rejected = _repository.Count(4, true, 0)
            };

            return ApiOk(result);
        }

        [Route("confirm")]
        [Route("~/covidapi/tokhais/confirm")]
        [HttpPost]
        public IHttpActionResult Confirm(ToKhaiConfirm confirm)
        {
            var tokhai = _repository.GetById(confirm.ToKhaiID);
            tokhai.TrangThaiToKhaiID = confirm.TrangThaiToKhaiID;
            var entity = _repository.Update(tokhai);

            var provinceTo = _areaRepository.GetByCode(entity.ProvinceCodeTo);
            var districtTo = _areaRepository.GetByCode(entity.DistrictCodeTo);
            var wardTo = _areaRepository.GetByCode(entity.WardCodeTo);

            var proviceToName = provinceTo == null ? "" : provinceTo.Name;
            var districtToName = districtTo == null ? "" : districtTo.Name;
            var wardToName = wardTo == null ? "" : wardTo.Name;

            if (confirm.TrangThaiToKhaiID == 4)
            {
                _notificationService.SendNotification(entity, proviceToName, districtToName, wardToName, confirm.TrangThaiToKhaiID);
            }
            

            return ApiOk(tokhai);
        }
    }
}
