/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class OrderTransferEntity : IEntity<OrderTransferEntity>, ICreationAudited, IDeleteAudited,
        IModificationAudited
    {
        public virtual string F_Id { get; set; }
        public string F_OrderId { get; set; } //订单ID
        public string F_TargetUserId { get; set; } //站点用户ID
        public int F_OrderTransferType { get; set; } //中转单类型
        public string F_TransferFee { get; set; } //中转费
        public string F_TransferCompany { get; set; } //中转公司名称
        public string F_TransferCompanyTelephone { get; set; } //中转公司电话
        public string F_TransferDescribe { get; set; } //中转信息
        public string F_PrintLabelNumber { get; set; } //打印数量
        public virtual int? F_SortCode { get; set; }
        public virtual bool? F_DeleteMark { get; set; }
        public virtual bool? F_EnabledMark { get; set; }
        public virtual string F_Description { get; set; }
        public virtual DateTime? F_CreatorTime { get; set; }
        public virtual string F_CreatorUserId { get; set; }
        public virtual DateTime? F_LastModifyTime { get; set; }
        public virtual string F_LastModifyUserId { get; set; }
        public virtual DateTime? F_DeleteTime { get; set; }
        public virtual string F_DeleteUserId { get; set; }
    }
}
