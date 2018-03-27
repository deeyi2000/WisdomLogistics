using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.OrderManage.Controllers
{
    public class OrderToVehicleController : ControllerBase
    {
        private readonly VehicleBindOrderApp _vehicleBindOrderApp = new  VehicleBindOrderApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _vehicleBindOrderApp.GetList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetValueGridJson(Pagination pagination, string keyValue, string keyword)
        {
            var data = new
            {
                rows = _vehicleBindOrderApp.GetList().Where(c => c.F_Id == keyValue),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _vehicleBindOrderApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(VehicleBindOrderEntity deviceEntity, string keyValue)
        {
            _vehicleBindOrderApp.SubmitForm(deviceEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitData(List<VehicleBindOrderEntity> deviceLists, string keyValue)
        {
            foreach (VehicleBindOrderEntity vehicleBindOrderEntity in deviceLists)
            {
                _vehicleBindOrderApp.SubmitForm(vehicleBindOrderEntity, keyValue);
            }
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _vehicleBindOrderApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            VehicleBindOrderEntity deviceEntity = new VehicleBindOrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = false;
            _vehicleBindOrderApp.UpdateForm(deviceEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            VehicleBindOrderEntity deviceEntity = new VehicleBindOrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = true;
            _vehicleBindOrderApp.UpdateForm(deviceEntity);
            return Success("账户启用成功。");
        }

        public ViewResult Load()
        {
            return View();
        }

        public ViewResult Jump()
        {
            return View();
        }
        public ViewResult JumpReceive()
        {
            return View();
        }

        public ViewResult Receive()
        {
            return View();
        }
    }
}