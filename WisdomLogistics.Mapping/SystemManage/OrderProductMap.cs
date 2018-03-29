using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Mapping.SystemManage
{
    public class   OrderProductMap : EntityTypeConfiguration<OrderProductEntity>
    {
        public OrderProductMap()
        {
            this.ToTable("Sys_OrderProduct");
            this.HasKey(t => t.F_Id);
            this.HasRequired(t => t.Order).WithMany(t => t.OrderProduct).HasForeignKey(t => t.F_OrderId);
        }
    }
}
