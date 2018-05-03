using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class SMSLogEntity : IEntity<SMSLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_OrderId { get; set; }
        //短信发送人电话
        public string F_Phone { get; set; }
        //短信内容
        public string F_Content { get; set; }       
        //是否发送成功
        public bool? F_isSendSuccess { get; set; }
        //是否验证成功
        public bool? F_VerificationSuccess { get; set; }
        //验证码
        public int F_Code { get; set; }


        /******  系统架构配置  ******/
        public int? F_SortCode { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
    }
}
