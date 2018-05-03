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
                //超级管理员
                if (LoginInfo.RoleId == null)
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                }
                //超级管理员
                if (LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                    list  = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                }
                //系统管理员
                else if (LoginInfo.RoleId == "4B2140D3-E61D-488E-ADF6-FF0EBCBC5D2C")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                }
                //物流服总公司
                else if (LoginInfo.RoleId == "6107fbb6-2a22-428d-a03a-b7b2e829d1a1")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                    list.Remove(list.Where(c => c.F_Id == "F0A2B36F-35A7-4660-B46C-D4AB796591EB").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "4B2140D3-E61D-488E-ADF6-FF0EBCBC5D2C").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "6107fbb6-2a22-428d-a03a-b7b2e829d1a1").SingleOrDefault());
                }
                //物流网点
                else if (LoginInfo.RoleId == "cf75118d-2a67-4fa7-ae71-4bf47a0fd6e2")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                    list = service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
                    list.Remove(list.Where(c => c.F_Id == "F0A2B36F-35A7-4660-B46C-D4AB796591EB").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "4B2140D3-E61D-488E-ADF6-FF0EBCBC5D2C").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "6107fbb6-2a22-428d-a03a-b7b2e829d1a1").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "cf75118d-2a67-4fa7-ae71-4bf47a0fd6e2").SingleOrDefault());
                    list.Remove(list.Where(c => c.F_Id == "278f8fc0-ac02-494f-990e-6acddf8cb643").SingleOrDefault());

                }
                //录单员
                else if (LoginInfo.RoleId == "cf75118d-2a67-4fa7-ae71-4bf47a0fd6e2")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                }
                //业务员
                else if (LoginInfo.RoleId == "71eb9b4b-def5-4cd2-935b-b28b259247ff")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                }
                //物流公司总财务
                else if (LoginInfo.RoleId == "278f8fc0-ac02-494f-990e-6acddf8cb643")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                }
                //网点财务
                else if (LoginInfo.RoleId == "40eaf22d-3489-4f5c-86a7-cdf94a7bdb85")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        expression = expression.Or(t => t.F_EnCode.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_Category == 1);
                }
            }
            /*
             if (!string.IsNullOrEmpty(keyword))
             {
                 expression = expression.And(t => t.F_FullName.Contains(keyword));
                 expression = expression.Or(t => t.F_EnCode.Contains(keyword));
             }
             */
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
            else
            {
                roleEntity.F_Id = Common.GuId();
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
