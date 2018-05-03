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
                if (null != LoginInfo)
                {
                    list = _serviceSMSLog.IQueryable().Where(c => c.F_CreatorUserId == LoginInfo.UserId).ToList();
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
