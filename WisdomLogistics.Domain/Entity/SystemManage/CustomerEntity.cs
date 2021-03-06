﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Domain.Entity.SystemManage
{
    public enum eCustomerType
    {
        Consignee, //收货人
        Shipments  //发货人
    }
    public class CustomerEntity : IEntity<CustomerEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }    //标识
        public string F_Name { get; set; }   //姓名
        public string F_Phone { get; set; }   //电话
        public string F_Address { get; set; }   //地址
        public string F_ProductName { get; set; } //产品名称
        public string F_Landline { get; set; }  //座机
        public eCustomerType F_CustomerType { get; set; }  //客户类型
        public string F_AccountOpening { get; set; }  //开户人
        public string F_OpeningBank { get; set; }  //开户行
        public string F_BankCardNumber { get; set; }     //银行卡号
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
