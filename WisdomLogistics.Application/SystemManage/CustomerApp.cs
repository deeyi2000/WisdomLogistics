﻿using System;
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
    public class CustomerApp
    {
        private ICustomerRepository service = new  CustomerRepository ();

        public List<CustomerEntity> GetList(string keyword = "")
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            var expression = ExtLinq.True<CustomerEntity>();
            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId.StartsWith("A_")) { }
                else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_Name.Contains(keyword));
                    expression = expression.Or(t => t.F_Phone.Contains(keyword));
                }
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public CustomerEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void RecordCustomer(OrderEntity orderEntity)
        {
            try
            {

            }
            catch
            {

            }
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(CustomerEntity vehicleEntity)
        {
            service.Update(vehicleEntity);
        }
        public void SubmitForm(CustomerEntity itemsEntity, string keyValue)
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