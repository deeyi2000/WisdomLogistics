using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.OrderManage.Controllers
{
    public class OrderListController : ControllerBase
    {
        private OrderApp deviceApp = new OrderApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = deviceApp.GetList(),
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
                rows = deviceApp.GetList().Where(c => c.F_Id == keyValue),
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
            var data = deviceApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public ActionResult GetOrderProductJson(string keyValue)
        {
           return Content(deviceApp.GetOrderProductList(keyValue).ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrderEntity deviceEntity, string keyValue)
        {
            deviceApp.SubmitForm(deviceEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            deviceApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledOrder(string keyValue)
        {
            OrderEntity deviceEntity = new OrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = false;
            deviceApp.UpdateForm(deviceEntity);
            return Success("订单作废成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledOrder(string keyValue)
        {
            OrderEntity deviceEntity = new OrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = true;
            deviceApp.UpdateForm(deviceEntity);
            return Success("订单恢复成功。");
        }
    }
}