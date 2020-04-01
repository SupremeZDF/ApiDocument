using System;

namespace ZJPayMent.DAL
{
    public class Class2501Request
    {
        /// <summary>
        /// 交易编号
        /// </summary>
        //public string TxCode { get; set; }

        /// <summary>
        /// 绑定流水号 （后续绑定支付和绑定代收需要使用）
        /// </summary>
        public string TxSNBinding { get; set; }

        /// <summary>
        /// 银行 ID，参考《银行编码表》
        /// </summary>
        public string BankID { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 账户号码
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// 开户证件类型 0=身份证 1=户口簿 2=护照 3=军官证 4=士兵证 
        /// 5=港澳居民来往内地通行证 6=台湾同胞来往内地通行证 7=临时身份证
        /// 8=外国人居留证 9=警官证 X = 其他证件
        /// </summary>
        public string IdentificationType { get; set; }
         
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 卡类型： 10=个人借记 20=个人贷记
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 信用卡有效期，格式 YYMM 绑定信用卡该项选填
        /// </summary>
        //public string ValidDate { get; set; }

        ///// <summary>
        ///// 信用卡背面的末 3 位数字 绑定信用卡该项选填
        ///// </summary>
        //public string CVN2 { get; set; }
    }
}
