using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Code;
using WisdomLogistics.Domain;
using WisdomLogistics.Domain.Entity.SystemManage;
using WisdomLogistics.Domain.IRepository.SystemManage;
using WisdomLogistics.Repository.SystemManage;

namespace WisdomLogistics.Application.SystemManage
{
   public class VehicleApp 
    {
        private IVehicleRepository service = new VehicleRepository();

        public List<VehicleEntity> GetList(string keyword = "")
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            var expression = ExtLinq.True<VehicleEntity>();
            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId.StartsWith("A_")) { }
                else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_Name.Contains(keyword));
                    expression = expression.Or(t => t.F_LicensePlate.Contains(keyword));
                    expression = expression.Or(t => t.F_Phone.Contains(keyword));
                }
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public VehicleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
                service.Delete(t => t.F_Id == keyValue);
        }

        public void UpdateForm(VehicleEntity vehicleEntity)
        {
            service.Update(vehicleEntity);
        }
        public void SubmitForm(VehicleEntity itemsEntity, string keyValue)
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
