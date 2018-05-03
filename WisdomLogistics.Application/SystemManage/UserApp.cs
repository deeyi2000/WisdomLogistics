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
using System;
using System.Collections.Generic;

namespace WisdomLogistics.Application.SystemManage
{
    public class UserApp
    {
        private IUserRepository service = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                //超级管理员
                if (LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //系统管理员
               else if (LoginInfo.RoleId == "4B2140D3-E61D-488E-ADF6-FF0EBCBC5D2C")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //物流服总公司
                else if (LoginInfo.RoleId == "6107fbb6-2a22-428d-a03a-b7b2e829d1a1")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //物流网点
                else if (LoginInfo.RoleId == "cf75118d-2a67-4fa7-ae71-4bf47a0fd6e2")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //录单员
                else if (LoginInfo.RoleId == "cf75118d-2a67-4fa7-ae71-4bf47a0fd6e2")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //业务员
                else if (LoginInfo.RoleId == "71eb9b4b-def5-4cd2-935b-b28b259247ff")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //物流公司总财务
                else if (LoginInfo.RoleId == "278f8fc0-ac02-494f-990e-6acddf8cb643")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
                //网点财务
                else if (LoginInfo.RoleId == "40eaf22d-3489-4f5c-86a7-cdf94a7bdb85")
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        expression = expression.And(t => t.F_Account.Contains(keyword));
                        expression = expression.Or(t => t.F_RealName.Contains(keyword));
                        expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
                    }
                    expression = expression.And(t => t.F_CreatorUserId == LoginInfo.UserId);
                }
            }

            return service.FindList(expression, pagination);
        }
        public UserEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public UserEntity GetUserNumber()
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            UserEntity userEntity = service.FindEntity(t => t.F_Id == LoginInfo.UserId);
            return userEntity;
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                if (LoginInfo.RoleId == "6107fbb6-2a22-428d-a03a-b7b2e829d1a1")
                {
                    UserEntity data = service.FindEntity(LoginInfo.UserId);
                   
                     var queryable  = service.IQueryable(c=>c.F_CreatorUserId ==LoginInfo.UserId) ;
                    if (data.F_AuthorizationQuantity >=0)
                    {
                        if (!string.IsNullOrEmpty(keyValue))
                        {
                            userEntity.Modify(keyValue);
                            UserEntity user = service.FindEntity(c => c.F_Id == keyValue);
                            userEntity.F_CreatorTime = user.F_CreatorTime;
                        }
                        else
                        {
                            userEntity.Create();
                            userEntity.F_ExpireTime = DateTime.Now;
                        }
                    

                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB" || LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB")
                        {
                            userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(int.Parse(userEntity.F_AuthorizationDays));
                            userEntity.F_DaysRemaining = int.Parse(userEntity.F_AuthorizationDays);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        userEntity.Modify(keyValue);
                        UserEntity user = service.FindEntity(c => c.F_Id == keyValue);
                        userEntity.F_CreatorTime = user.F_CreatorTime;
                    }
                    else
                    {
                        userEntity.Create();
                        userEntity.F_ExpireTime = DateTime.Now;
                    }

                    //超级管理员 || 系统管理员
                    if (LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB" || LoginInfo.RoleId == "F0A2B36F-35A7-4660-B46C-D4AB796591EB")
                    {
                        userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(int.Parse(userEntity.F_AuthorizationDays));
                        userEntity.F_DaysRemaining = int.Parse(userEntity.F_AuthorizationDays);
                    }
                }
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }
        public void UpdateForm(UserEntity userEntity)
        {
            service.Update(userEntity);
        }

        public UserEntity CheckLogin(string username, string password)
        {
            UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
            if (userEntity != null)
            {
                if (userEntity.F_EnabledMark == true)
                {
                    UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userLogOnEntity.F_UserPassword)
                    {
                        DateTime lastVisitTime = DateTime.Now;
                        int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                        if (userLogOnEntity.F_LastVisitTime != null)
                        {
                            userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                        }
                        userLogOnEntity.F_LastVisitTime = lastVisitTime;
                        userLogOnEntity.F_LogOnCount = LogOnCount;
                        userLogOnApp.UpdateForm(userLogOnEntity);
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}
