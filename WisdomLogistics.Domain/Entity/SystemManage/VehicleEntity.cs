using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class VehicleEntity : IEntity<VehicleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string F_LicensePlate { get; set; }  //车牌号码

        public string F_DLNumber { get; set; }  //行驶证号

        public string F_Name { get; set; } //   司机姓名

        public string F_Phone { get; set; }  //   司机电话

        public List<VehicleBindOrderEntity> VehicleBindOrder { get; set; }

        //必备字段
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
