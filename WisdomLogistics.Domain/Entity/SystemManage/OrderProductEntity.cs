using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class OrderProductEntity : IEntity<RoleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public virtual string F_Id { get; set; }
        //货物名称
        public virtual string F_Name { get; set; }
        //件数
        public virtual string F_Number { get; set; }
        //重量
        public virtual string F_Weight { get; set; }
        //体积
        public virtual string F_Volume { get; set; }
        //单价
        public virtual string F_UnitPricee { get; set; }
        //运费
        public virtual string F_Freight { get; set; }
        //送货费
        public virtual string F_DeliveryFee { get; set; }
        //提货费
        public virtual string F_PickDeliveryFee { get; set; }
        //保价额
        public virtual string F_InsuredForehead { get; set; }
        //保价费
        public virtual string F_InsuredFee { get; set; }
        public virtual string F_OrderId { get; set; }
        public virtual OrderEntity Order { get; set; }
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
