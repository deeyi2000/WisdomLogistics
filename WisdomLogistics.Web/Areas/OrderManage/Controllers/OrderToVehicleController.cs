/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.OrderManage.Controllers
{
    public class OrderToVehicleController : ControllerBase
    {
        private readonly VehicleBindOrderApp _vehicleBindOrderApp = new  VehicleBindOrderApp();
        private readonly OrderApp _orderApp = new OrderApp();
        private readonly VehicleApp _vehicleApp = new VehicleApp();

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
        public ActionResult GetValueGridJson(Pagination pagination, string keyValue, string keyword, int dataType)
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
        public ActionResult SubmitData(VehicleBindOrderEntity  vehicleBindOrder, List<OrderEntity> LoadOrderItems, string keyValue)
        {
            foreach (OrderEntity loadOrderItem in LoadOrderItems)
            {
                OrderEntity order = _orderApp.GetForm(loadOrderItem.F_Id);
                order.F_IsLoading = true;
                order.transportState = eTransportState.Loading;
                order.loadTime = DateTime.Now;
                _orderApp.UpdateForm(order);
                VehicleBindOrderEntity vehicleBind = new VehicleBindOrderEntity() { F_CarFreight = vehicleBindOrder.F_CarFreight, F_Description = vehicleBindOrder.F_Description, F_EnabledMark = vehicleBindOrder.F_EnabledMark, F_DriverPhone = vehicleBindOrder.F_DriverPhone, F_VehicleId = vehicleBindOrder.F_VehicleId, F_LicensePlate = vehicleBindOrder.F_LicensePlate, F_Station = vehicleBindOrder.F_Station, F_OrderId = order.F_Id };
                _vehicleBindOrderApp.SubmitForm(vehicleBind, keyValue);
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