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
    public class OrderApp
    {
        private readonly IOrderRepository _service = new OrderRepository();
        private readonly IOrderProductRepository _serviceOederProduct = new OrderProductRepository();
        private readonly OrderQuantityApp _orderQuantityApp = new OrderQuantityApp();

        //获取当前用户所有订单
        public List<OrderEntity> GetList(eOrderDataType dataType)
        {
            List<OrderEntity> lists = new List<OrderEntity>();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                var expression = ExtLinq.True<OrderEntity>();

                if (LoginInfo != null)
                {
                    if (LoginInfo.RoleId.StartsWith("A_")) { }
                    else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                    else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                    else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                    lists = _service.IQueryable(expression).Where(c => c.F_DataType == dataType).ToList();
                    foreach (OrderEntity list in lists)
                    {
                        list.OrderProduct = _serviceOederProduct.IQueryable().Where(c => c.F_OrderId == list.F_Id).ToList();
                    }
                  return lists;
                }
                else
                {
                    return lists;
                }
            }
            catch(Exception e)
            {
                return lists;
            }
       }

        /*
        //获取当前用户已经装车的订单
        public List<OrderEntity> GetLoadingList()
        {
            List<OrderEntity> lists = new List<OrderEntity>();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    lists = service.IQueryable().Where(c => c.F_CreatorUserId == LoginInfo.UserId).Where(c => c.F_IsLoading == true).ToList();
                    foreach (OrderEntity list in lists)
                    {
                        list.OrderProduct = serviceOederProduct.IQueryable().Where(c => c.F_OrderId == list.F_Id).ToList();
                    }
                    return lists;
                }
                else
                {
                    return lists;
                }
            }
            catch
            {
                return lists;
            }  
        }

        //获取当前用户已经转运的订单
        public List<OrderEntity> GetTransferList()
        {

            List<OrderEntity> lists = new List<OrderEntity>();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    lists = service.IQueryable().Where(c => c.F_CreatorUserId == LoginInfo.UserId).Where(c => c.F_IsTransfer == true).ToList();
                    foreach (OrderEntity list in lists)
                    {
                        list.OrderProduct = serviceOederProduct.IQueryable().Where(c => c.F_OrderId == list.F_Id).ToList();
                    }
                    return lists;
                }
                else
                {
                    return lists;
                }
            }
            catch
            {
                return lists;
            }
        }

        //获取当前用户已经卸车的订单
        public List<OrderEntity> GetUnLoadingList()
        {
            List<OrderEntity> lists = new List<OrderEntity>();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    lists = service.IQueryable().Where(c => c.F_CreatorUserId == LoginInfo.UserId).Where(c => c.F_IsUnLoading == true).ToList();
                    foreach (OrderEntity list in lists)
                    {
                        list.OrderProduct = serviceOederProduct.IQueryable().Where(c => c.F_OrderId == list.F_Id).ToList();
                    }
                    return lists;
                }
                else
                {
                    return lists;
                }
            }
            catch
            {
                return lists;
            }
        }
        */

        public OrderEntity GetForm(string keyValue)
        {
            OrderEntity order = new OrderEntity();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    order = _service.FindEntity(keyValue);
                    List<OrderProductEntity> orderProduct = _serviceOederProduct.IQueryable(c => c.F_OrderId == order.F_Id).ToList();
                    order.OrderProduct = orderProduct;
                    return order;
                }
                else
                {
                    return order;
                }
            }
            catch
            {
                return order;
            }
        }
        public void DeleteForm(string keyValue)
        {
            try
            {
                _service.Delete(t => t.F_Id == keyValue);
            }
            catch
            {

            }
        }

        public void UpdateForm(OrderEntity orderEntity)
        {
            try
            {
                    _service.Update(orderEntity);
            }
            catch
            {

            }
        }

        public List<OrderProductEntity> GetOrderProductList(string keyValue)
        {
            List<OrderProductEntity> lists = _serviceOederProduct.IQueryable().Where(c => c.F_OrderId == keyValue)
                .ToList();
            return lists;
        }

        public void SubmitForm(OrderEntity orderEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                orderEntity.F_ArtNo = orderEntity.F_ArtNo01 + "-" + orderEntity.F_ArtNo02 + "-" + orderEntity.F_ArtNo03;
                orderEntity.F_WaybillNumber = null;
                orderEntity.F_CreatorTime = null;
                orderEntity.F_CreatorUserId = null;
                orderEntity.F_EnabledMark = null;
                orderEntity.Modify(keyValue);
                _service.Update(orderEntity);
            }
            else
            {
                orderEntity.Create();
                orderEntity.F_WaybillNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff ");    //+ Common.RndNum(4);
                orderEntity.F_ArtNo = orderEntity.F_ArtNo01 + "-" + orderEntity.F_ArtNo02 + "-" + orderEntity.F_ArtNo03;
                orderEntity.F_EnabledMark = true;
                               for (int i = orderEntity.OrderProduct.Count - 1; i >= 0; --i)
                {
                    OrderProductEntity orderProducts = orderEntity.OrderProduct[i];
                    orderProducts.Create();
                    orderEntity.OrderProduct[i] = orderProducts;
                }
                _service.Insert(orderEntity);
                _orderQuantityApp.UpdateOrderQuantity();
            }
        }
    }
}
