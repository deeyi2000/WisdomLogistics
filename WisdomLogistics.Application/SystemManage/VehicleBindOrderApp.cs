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
        private IVehicleBindOrderRepository service = new VehicleBindOrderRepository();
        public List<VehicleBindOrderEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<VehicleBindOrderEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                //expression = expression.And(t => t.F_Name.Contains(keyword));
                //expression = expression.Or(t => t.F_LicensePlate.Contains(keyword));
                //expression = expression.Or(t => t.F_Phone.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public VehicleBindOrderEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(VehicleBindOrderEntity vehicleEntity)
        {
            service.Update(vehicleEntity);
        }
        public void SubmitForm(VehicleBindOrderEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.F_OrderId = itemsEntity.F_Id;
                itemsEntity.Create();
                service.Insert(itemsEntity);
            }
        }
    }
}
