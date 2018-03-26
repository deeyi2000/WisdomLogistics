using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.OrderManage.Controllers
{
    /// <summary>
    /// 订单中转控制器
    /// </summary>
    public class OrderToTransferController : ControllerBase
    {
        private readonly OrderTransferApp _orderTransfer = new OrderTransferApp();
        private readonly OrderApp _order = new OrderApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _orderTransfer.GetList(),
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
                rows = _orderTransfer.GetList().Where(c => c.F_Id == keyValue),
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
            var data = _orderTransfer.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrderTransferEntity orderTransferEntity, string keyValue)
        {
            _orderTransfer.SubmitForm(orderTransferEntity, keyValue);
            OrderEntity orderEntity = _order.GetForm(orderTransferEntity.F_OrderId);
            orderEntity.F_IsTransfer = true;
            _order.UpdateForm(orderEntity);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _orderTransfer.DeleteForm(keyValue);
            return Success("删除成功。");

        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledOrderTransfer(string keyValue)
        {
            OrderTransferEntity orderTransferEntity = new OrderTransferEntity();
            orderTransferEntity.F_Id = keyValue;
            orderTransferEntity.F_EnabledMark = false;
            _orderTransfer.UpdateForm(orderTransferEntity);
            return Success("中转单禁用成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledOrderTransfer(string keyValue)
        {
            OrderTransferEntity orderTransferEntity = new OrderTransferEntity();
            orderTransferEntity.F_Id = keyValue;
            orderTransferEntity.F_EnabledMark = true;
            _orderTransfer.UpdateForm(orderTransferEntity);
            return Success("中转单启用成功。");
        }
    }
}