using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Domain.Entity.SystemManage;

namespace WisdomLogistics.Mapping.SystemManage
{
    public class VehicleBindOrderMap : EntityTypeConfiguration<VehicleBindOrderEntity>
    {
        public VehicleBindOrderMap()
        {
            this.ToTable("SysVehicleBindOrder");
            this.HasKey(t => t.F_Id);
        }
    }
}
