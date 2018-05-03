/*******************************************************************************
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
    public enum eOrderDataType
    {
        OrderStart = 0,        //订单创建
        Loading = 1,           //订单装车
        Transportation = 2,    //订单运输
        Transfer = 3,          //订单中转
        UnLoading = 4,         //订单卸载
        OrderEnd = 5           //订单结束
    }

    public enum eTransportState
    {   //等待装车
        WaitLoad =0,
        //已经装车
        Loading =1,
        //等待发车
        WaitDepartureVehicle =2,
        //运输途中
        Transit =3,
        //已运抵
        HaveArrived =4

    }
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
        public string F_ArtNo01 { get; set; }
        //货号-002
        public string F_ArtNo02 { get; set; }
        //货号--003
        public string F_ArtNo03 { get; set; }
        //货号
        public string F_ArtNo { get; set; }
        //运单号
        public string F_WaybillNumber { get; set ; }
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
        public string F_DarkPad { get; set; }
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
        public bool F_Receipt { get; set; }
        //上门送货
        public bool F_DeliveryHome { get; set; }
        //针式打印
        public bool F_DirectThermal { get; set; }
        //针式打印 数量
        public string F_NeedlePrinterNumber { get; set; }
        //打印标签
        public bool F_PrintLabel { get; set; }
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
        //是否装车
        public bool F_IsLoading { get; set; }
        //是否卸车
        public bool F_IsUnLoading { get; set; }
        //是否转运
        public  bool F_IsTransfer { get; set; }
        //订单类型状态
        public eOrderDataType F_DataType { get; set; }
        //车辆车牌号
        public string F_LicensePlate { get; set; }
        //车辆运费
        public string F_CarFreight { get; set; }
        //运输状态
        public eTransportState transportState { get; set; }
        //接车时间 
        public DateTime? ArrivalTime { get; set; }
        //装车时间
        public DateTime? loadTime { get; set; }
        //发车时间
        public DateTime? DepartureTime { get; set; }

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
