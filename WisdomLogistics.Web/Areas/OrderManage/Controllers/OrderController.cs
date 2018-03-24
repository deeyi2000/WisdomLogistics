using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.OrderManage.Controllers
{
    public class OrderController : ControllerBase
    {
        private OrderApp orderApp = new OrderApp();
        private OrderQuantityApp orderQuantityApp = new OrderQuantityApp();
        private UserApp userApp = new UserApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = orderApp.GetList(),
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
                rows = orderApp.GetList().Where(c => c.F_Id == keyValue),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetOrderQuantity()
        {
           return Content(orderQuantityApp.GetOrderQuantity().ToJson());
        }

        public ActionResult GetUserNumber()
        {
            return Content(userApp.GetUserNumber().ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = orderApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrderEntity deviceEntity, string keyValue)
        {
            orderApp.SubmitForm(deviceEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            orderApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            OrderEntity deviceEntity = new OrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = false;
            orderApp.UpdateForm(deviceEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            OrderEntity deviceEntity = new OrderEntity();
            deviceEntity.F_Id = keyValue;
            deviceEntity.F_EnabledMark = true;
            orderApp.UpdateForm(deviceEntity);
            return Success("账户启用成功。");
        }
    }
}