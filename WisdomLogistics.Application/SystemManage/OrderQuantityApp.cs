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
    public class OrderQuantityApp
    {
        private IOrderQuantityRepository service = new OrderQuantityRepository();

        public OrderQuantityEntity GetOrderQuantity()
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (!service.FindEntity(c => c.F_CreatorUserId == LoginInfo.UserId).IsEmpty())
            {
                OrderQuantityEntity orderQuantityEntity = service.IQueryable(t => t.F_CreatorUserId == LoginInfo.UserId).SingleOrDefault();
                orderQuantityEntity.F_OrderQuantity = orderQuantityEntity.F_OrderQuantity + 1;
                return orderQuantityEntity;
            }
            else
            {
                UpdateOrderQuantity();
                return service.IQueryable(t => t.F_CreatorUserId == LoginInfo.UserId).SingleOrDefault();
            }
        }
        public void UpdateOrderQuantity()
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (service.FindEntity(c => c.F_CreatorUserId == LoginInfo.UserId).IsEmpty())
            {
                OrderQuantityEntity orderQuantityEntity = new OrderQuantityEntity() { F_OrderQuantity = 1 };
                orderQuantityEntity.Create();
                service.Insert(orderQuantityEntity);

            }
            else
            {
                OrderQuantityEntity orderQuantityEntity = service.IQueryable(t => t.F_CreatorUserId == LoginInfo.UserId).SingleOrDefault();
                orderQuantityEntity.F_OrderQuantity = orderQuantityEntity.F_OrderQuantity + 1;
                service.Update(orderQuantityEntity);
            }
        }
    }
}
