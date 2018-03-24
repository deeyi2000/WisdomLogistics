using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class OrderQuantityEntity : IEntity<OrderQuantityEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }    //标识
        public int F_OrderQuantity { get; set; }   //订单编号
        public int? F_SortCode { get; set; }    //排序号
        public bool? F_DeleteMark { get; set; }   //删除标记
        public bool? F_EnabledMark { get; set; }   //启用/禁用
        public string F_Description { get; set; }    //描述
        public DateTime? F_CreatorTime { get; set; }   //创建时间
        public string F_CreatorUserId { get; set; }    //创建用户
        public DateTime? F_LastModifyTime { get; set; }    //修改时间
        public string F_LastModifyUserId { get; set; }    //修改用户
        public DateTime? F_DeleteTime { get; set; }     // 删除时间
        public string F_DeleteUserId { get; set; }      //删除用户
    }
}
