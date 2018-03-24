using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class OrderTransferEntity: IEntity<OrderTransferEntity>,ICreationAudited,IDeleteAudited,IModificationAudited
    {
        public virtual string F_Id { get; set; }

        public string F_OrderId { get; set; }  //订单ID

        public string F_TargetUserId { get; set; }   //站点用户ID

        public int F_OrderTransferType { get; set; }   //中转单类型

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
