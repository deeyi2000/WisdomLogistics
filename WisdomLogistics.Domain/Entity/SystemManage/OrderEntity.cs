using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public class OrderEntity : IEntity<OrderEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        //到站
        public string F_Station { get; set; }
        //收货人
        public string F_Consignee { get; set; }
        //收货人电话
        public string F_ConsigneePhone { get; set; }
        //货号--001
        public string ArtNo01 { get; set; }
        //货号-002
        public string ArtNo02 { get; set; }
        //货号--003
        public string ArtNo03 { get; set; }
        //地区
        public string F_Area { get; set; }
        //发货人
        public string F_Consignor { get; set; }
        //发货人电话
        public string F_ConsignorPhone { get; set; }
        //运费合计
        public string F_FreightCombined { get; set; }
        //支付类型
        public string F_PayType { get; set; }
        //付款方式
        public string F_PaymentMethod { get; set; }
        //提付金额
        public string F_PutPay { get; set; }
        //现付金额
        public string F_PresentPay { get; set; }
        //回付金额
        public string F_BackPay { get; set; }
        //明垫
        public string F_BrightPad { get; set; }
        //明垫付款状态
        public string F_BrightPadPaymentState { get; set; }
        //暗垫
        public string G_DarkPad { get; set; }
        //暗垫付款状态
        public string F_DarkMatPaymentState { get; set; }
        //代收货款
        public string F_CollectionPayment { get; set; }
        //结算方式
        public string F_SettlementMode { get; set; }
        //手续费率
        public string F_FormalityRate { get; set; }
        //放款网点
        public string F_LendingOutlets { get; set; }
        //开户人
        public string F_AccountOpening { get; set; }
        //开户行
        public string F_OpeningBank { get; set; }
        //银行卡号
        public string F_BankCardNumber { get; set; }
        //需要回单
        public string F_Receipt { get; set; }
        //上门送货
        public string F_DeliveryHome { get; set; }
        //备注
        public string F_Remarks { get; set; }
        //针式打印
        public string F_DirectThermal { get; set; }
        //针式打印 数量
        public string F_NeedlePrinterNumber { get; set; }
        //打印标签
        public string F_PrintLabel { get; set; }
        //打印标签 数量
        public string F_PrintLabelNumber { get; set; }
        //运单套打
        public string F_Waybill { get; set; }
        //录单人
        public string F_RecordSingle { get; set; }
        //业务员
        public string F_Salesman { get; set; }
        //车辆备注
        public string F_VehicleRemarks { get; set; }
        public List<OrderProductEntity> OrderProduct { get; set; }
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
