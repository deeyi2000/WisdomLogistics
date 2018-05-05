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
   public class VehicleBindOrderApp
    {
        private readonly IVehicleBindOrderRepository _serviceVehicleBindOrder = new VehicleBindOrderRepository();
        private readonly IOrderRepository _serviceOrder = new OrderRepository();

        public List<VehicleBindOrderEntity> GetList(string keyword = "")
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            var expression = ExtLinq.True<VehicleBindOrderEntity>();
            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId.StartsWith("A_")) { }
                else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_DriverPhone.Contains(keyword));
                    expression = expression.Or(t => t.F_LicensePlate.Contains(keyword));
                    expression = expression.Or(t => t.F_Description.Contains(keyword));
                }
            }
            return _serviceVehicleBindOrder.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public VehicleBindOrderEntity GetForm(string keyValue)
        {
            return _serviceVehicleBindOrder.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _serviceVehicleBindOrder.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(VehicleBindOrderEntity vehicleEntity)
        {
            _serviceVehicleBindOrder.Update(vehicleEntity);
        }
        public void SubmitForm(VehicleBindOrderEntity itemsEntity, string keyValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    itemsEntity.Modify(keyValue);
                    _serviceVehicleBindOrder.Update(itemsEntity);
                }
                else
                {
                    itemsEntity.Create();
                    _serviceVehicleBindOrder.Insert(itemsEntity);
                    OrderEntity order = _serviceOrder.FindEntity(itemsEntity.F_OrderId);
                    order.F_DataType = eOrderDataType.Loading;
                    _serviceOrder.Update(order);
                }
            }
            catch (Exception ex) {
                
            }
        }
    }
}
