using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.VehicleManage.Controllers
{
    public class ArrivalVehicleController : ControllerBase
    {
        private OrderApp deviceApp = new OrderApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, int dataType)
        {
            var data = new
            {
                rows = deviceApp.GetList((eOrderDataType)System.Enum.ToObject(typeof(eOrderDataType), dataType)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Arrival(string keyValue)
        {
            OrderEntity deviceEntity = deviceApp.GetList(eOrderDataType.Transportation).Where(c => c.F_Id == keyValue).SingleOrDefault();
            deviceEntity.F_DataType = eOrderDataType.UnLoading;
            deviceEntity.ArrivalTime = DateTime.Now;
            deviceApp.UpdateForm(deviceEntity);
            return Success("接车成功。");
        }
    }
}