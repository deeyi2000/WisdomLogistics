using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisdomLogistics.Data;
using WisdomLogistics.Domain.Entity.SystemManage;
using WisdomLogistics.Domain.IRepository.SystemManage;

namespace WisdomLogistics.Repository.SystemManage
{
   public class VehicleRepository : RepositoryBase<VehicleEntity>, IVehicleRepository
    {
    }
}
