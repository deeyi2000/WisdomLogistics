/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;
using WisdomLogistics.Domain.IRepository.SystemManage;
using WisdomLogistics.Repository.SystemManage;

namespace WisdomLogistics.Application.SystemManage
{
   public class OrderTransferApp
    {
        private IOrderTransferRepository service = new OrderTransferRepository();
        public List<OrderTransferEntity> GetList(string keyword = "")
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            var expression = ExtLinq.True<OrderTransferEntity>();

            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId.StartsWith("A_")) { }
                else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_TransferCompanyTelephone.Contains(keyword));
                    expression = expression.Or(t => t.F_TransferDescribe.Contains(keyword));
                    expression = expression.Or(t => t.F_TransferCompany.Contains(keyword));
                }
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public OrderTransferEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(OrderTransferEntity vehicleEntity)
        {
            service.Update(vehicleEntity);
        }
        public void SubmitForm(OrderTransferEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.Create();
                service.Insert(itemsEntity);
            }
        }
    }
}
