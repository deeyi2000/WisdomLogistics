using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WisdomLogistics.Application.SystemManage;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Web.Areas.CustomerManage.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly CustomerApp _customerApp = new CustomerApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _customerApp.GetList(keyword),
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
                rows = _customerApp.GetList().Where(c => c.F_Id == keyValue),
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
            var data = _customerApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CustomerEntity customerEntity, string keyValue)
        {
            customerEntity.F_EnabledMark = true;
            _customerApp.SubmitForm(customerEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _customerApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledCustomer(string keyValue)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            customerEntity.F_Id = keyValue;
            customerEntity.F_EnabledMark = false;
            _customerApp.UpdateForm(customerEntity);
            return Success("客户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledCustomer(string keyValue)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            customerEntity.F_Id = keyValue;
            customerEntity.F_EnabledMark = true;
            _customerApp.UpdateForm(customerEntity);
            return Success("客户启用成功。");
        }
    }
}