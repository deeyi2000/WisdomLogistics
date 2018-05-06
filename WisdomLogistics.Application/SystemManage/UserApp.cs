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
            if (LoginInfo != null && LoginInfo.RoleId.EndsWith("_admin"))
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_Account.Contains(keyword));
                    expression = expression.Or(t => t.F_RealName.Contains(keyword));
                }
                expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
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

        public List<UserEntity> GetCompanyStatonMember(string keyword)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
            }
            expression = expression.And(u => u.F_CompanyId.Contains(LoginInfo.CompanyId)).And(t => t.F_RoleId.StartsWith("S_"));
            return service.FindList(expression);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public string SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //判断角色
            if (LoginInfo.RoleId.StartsWith("C_"))
            {
                //获取当前用户创建的站点数量
                List<UserEntity> item = service.FindList(ExtLinq.True<UserEntity>().And(t => t.F_CreatorUserId.Contains(LoginInfo.UserId)).And(t => t.F_RoleId.StartsWith("S")));
                if (item.Count < LoginInfo.AuthorizationQuantity)
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        userEntity.Modify(keyValue);
                        UserEntity user = service.FindEntity(c => c.F_Id == keyValue);
                        userEntity.F_CreatorTime = user.F_CreatorTime;

                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId.EndsWith("_admin"))
                        {
                            if (!userEntity.F_AuthorizationDays.IsEmpty())
                            {
                                userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                                userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
                            }
                        }
                        service.SubmitForm(userEntity, userLogOnEntity, keyValue);
                        return "修改完成";
                    }
                    else
                    {
                        userEntity.Create();
                        if (userEntity.F_RoleId.StartsWith("A_")) { }
                        else if (userEntity.F_RoleId.StartsWith("C_"))
                        {
                            userEntity.F_CompanyId =LoginInfo.UserId;
                        }
                        else if (userEntity.F_RoleId.StartsWith("S_"))
                        {
                            userEntity.F_StationId = userEntity.F_Id;
                            userEntity.F_CompanyId = LoginInfo.CompanyId;
                        }
                        else
                        {
                            userEntity.F_StationId = userEntity.F_StationId;
                            userEntity.F_CompanyId = LoginInfo.CompanyId;
                        }
                        userEntity.F_ExpireTime = DateTime.Now;
                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId.EndsWith("_admin"))
                        {
                            if (!userEntity.F_AuthorizationDays.IsEmpty())
                            {
                                userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                                userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
                            }
                        }
                        service.SubmitForm(userEntity, userLogOnEntity, keyValue);
                        return "创建完成";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        userEntity.Modify(keyValue);
                        UserEntity user = service.FindEntity(c => c.F_Id == keyValue);
                        userEntity.F_CreatorTime = user.F_CreatorTime;
                        userEntity.F_ExpireTime = DateTime.Now;
                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId.EndsWith("_admin"))
                        {
                            if (!userEntity.F_AuthorizationDays.IsEmpty())
                    if (0 != userEntity.F_AuthorizationDays)
                            {
<<<<<<< .mine
                                userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                                userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
=======
                        userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                        userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
>>>>>>> .theirs
                            }
                        }
                        service.SubmitForm(userEntity, userLogOnEntity, keyValue);
                        return "修改完成";
                    }
                    else
                    {
                        return "站点数量超出授权，请联系管理员！";
                    }
                }
            }
            else
            {
                if (LoginInfo != null && LoginInfo.RoleId.EndsWith("_admin"))
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        userEntity.Modify(keyValue);
                        UserEntity user = service.FindEntity(c => c.F_Id == keyValue);
                        userEntity.F_CreatorTime = user.F_CreatorTime;
                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId.EndsWith("_admin"))
                        {
                            if (!userEntity.F_AuthorizationDays.IsEmpty())
                            {
                                userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                                userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
                            }
                        }
                        service.SubmitForm(userEntity, userLogOnEntity, keyValue);
                        return "修改完成";
                    }
                    else
                    {
                        userEntity.Create();
                        //补充角色
                        if (userEntity.F_RoleId.StartsWith("A_")) { }

                        else if (userEntity.F_RoleId.StartsWith("C_"))
                        {
                            userEntity.F_CompanyId =LoginInfo.UserId;
                        }
                        else if (userEntity.F_RoleId.StartsWith("S_"))
                        {
                            userEntity.F_StationId = userEntity.F_Id;
                            userEntity.F_CompanyId = LoginInfo.CompanyId;
                        }
                        else
                        {
                            userEntity.F_StationId = userEntity.F_StationId;
                            userEntity.F_CompanyId = LoginInfo.CompanyId;
                        }

                        userEntity.F_ExpireTime = DateTime.Now;
                        //超级管理员 || 系统管理员
                        if (LoginInfo.RoleId.EndsWith("_admin"))
                        {
                            if (!userEntity.F_AuthorizationDays.IsEmpty())
                            {
                                userEntity.F_ExpireTime = ((DateTime)userEntity.F_CreatorTime).AddDays(userEntity.F_AuthorizationDays);
                                userEntity.F_DaysRemaining = userEntity.F_AuthorizationDays;
                            }
                        }
                        service.SubmitForm(userEntity, userLogOnEntity, keyValue);
                        return "创建完成";
                    }
                }
                else
                {
                    return "请联系管理员";
                }
            }
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
