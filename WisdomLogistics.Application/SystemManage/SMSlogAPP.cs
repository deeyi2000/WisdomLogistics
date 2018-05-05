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
   public class SMSlogAPP
    {
        private readonly ISMSLogRepository _serviceSMSLog = new SMSLogRepository();

        //获取当前用户所有短信日志
        public List<SMSLogEntity> GetList()
        {
            List<SMSLogEntity> list = new List<SMSLogEntity>();
            try
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                var expression = ExtLinq.True<SMSLogEntity>();
                if (null != LoginInfo)
                {
                    if (LoginInfo.RoleId.StartsWith("A_")) { }
                    else if (LoginInfo.RoleId.StartsWith("C_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.CompanyId));
                    else if (LoginInfo.RoleId.StartsWith("S_")) expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.StationId));
                    else expression = expression.And(u => u.F_CreatorUserId.Contains(LoginInfo.UserId));
                    list = _serviceSMSLog.IQueryable(expression).ToList();
                    return list;
                }
                else
                {
                    return list;
                }
            }
            catch
            {
                return list;
            }
        }
    }
}
