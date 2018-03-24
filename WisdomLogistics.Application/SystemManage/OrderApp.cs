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
        private IOrderRepository service = new OrderRepository();
        private IOrderProductRepository serviceOederProduct = new OrderProductRepository();
        private OrderQuantityApp orderQuantityApp = new OrderQuantityApp();

        public List<OrderEntity> GetList()
        {
            List<OrderEntity> lists = service.IQueryable().ToList();
            foreach (OrderEntity list in lists)
            {
                list.OrderProduct = serviceOederProduct.IQueryable().Where(c => c.F_OrderId == list.F_Id).ToList();
            }
            return lists;
        }
        public OrderEntity GetForm(string keyValue)
        {
            OrderEntity order = service.FindEntity(keyValue);
            List<OrderProductEntity> orderProduct = serviceOederProduct.IQueryable(c => c.F_OrderId == order.F_Id).ToList();
            order.OrderProduct = orderProduct;
            return order;
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(OrderEntity orderEntity)
        {
            service.Update(orderEntity);
        }

        public void SubmitForm(OrderEntity orderEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                orderEntity.Modify(keyValue);
                service.Update(orderEntity);
            }
            else
            {
                orderEntity.Create();
                orderEntity.F_WaybillNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff ");    //+ Common.RndNum(4);
                orderEntity.F_ArtNo = orderEntity.F_ArtNo01 + "-" + orderEntity.F_ArtNo02 + "-" + orderEntity.F_ArtNo03;
                for (int i = orderEntity.OrderProduct.Count - 1; i >= 0; --i)
                {
                    OrderProductEntity orderProducts = orderEntity.OrderProduct[i];
                    orderProducts.Create();
                    orderEntity.OrderProduct[i] = orderProducts;
                }
                service.Insert(orderEntity);
                orderQuantityApp.UpdateOrderQuantity();
            }
        }
    }
}
