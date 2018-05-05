/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using WisdomLogistics.Code;
using WisdomLogistics.Domain.Entity.SystemManage;
using WisdomLogistics.Domain.IRepository.SystemManage;
using WisdomLogistics.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq;

namespace WisdomLogistics.Application.SystemManage
{
    public class RoleApp
    {
        private IRoleRepository service = new RoleRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            List<RoleEntity> list = new List<RoleEntity>();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId ==null)
                {
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                }
               else if (LoginInfo.RoleId.StartsWith("A_"))
                {
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("A_")).ToList()) { list.Remove(item); }
                }
                else if (LoginInfo.RoleId.StartsWith("C_")) {
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("A_")).ToList()) { list.Remove(item); }
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("S_")).ToList()) { list.Remove(item); }
                }
                else if (LoginInfo.RoleId.StartsWith("S_")) {
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("A_")).ToList()) { list.Remove(item); }
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("C_")).ToList()) { list.Remove(item); }
                    foreach (var item in list.Where(c => c.F_Id.StartsWith("S_")).ToList()) { list.Remove(item); }
                }
                else
                {

                }
            }
            return list;
        }
        public RoleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.F_Id = Common.GuId();
                roleAuthorizeEntity.F_ObjectType = 1;
                roleAuthorizeEntity.F_ObjectId = roleEntity.F_Id;
                roleAuthorizeEntity.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);
        }
    }
}
