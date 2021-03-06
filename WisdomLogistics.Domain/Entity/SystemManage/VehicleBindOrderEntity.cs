﻿/*******************************************************************************
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
    public class VehicleBindOrderEntity : IEntity<VehicleBindOrderEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public virtual string F_Id { get; set; }
        public string F_OrderId { get; set; }  //订单ID
        public string F_VehicleId { get; set; }  //车辆ID
        public string F_TargetUserId { get; set; } //站点用户ID
        public string F_Station { get; set; }  //到站名称
        public string F_LicensePlate { get; set; } //车牌号
        public string F_DriverPhone { get; set; }  //司机电话
        public int F_CarFreight { get; set; }   //车运费

        //必备字段
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
