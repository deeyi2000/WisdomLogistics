/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Mapping.SystemManage
{
    class OrderMap: EntityTypeConfiguration<OrderEntity>
    {
        public OrderMap()
        {
            this.ToTable("Sys_Order");
            this.HasKey(t => t.F_Id);
            this.HasMany(t => t.OrderProduct).WithRequired(t => t.Order).HasForeignKey(t => t.F_OrderId);
        }
    }
}
