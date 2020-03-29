using System;
using System.Collections.Generic;
using System.Text;

namespace ZJPayMent.DAL
{
    public class Class2501Response
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        public string InstitutionID { get; set; }

        /// <summary>
        /// 绑定流水号
        /// </summary>
        public string TxSNBinding { get; set; }

        /// <summary>
        /// 交易状态 10=绑定处理中 20=绑定失败 30=绑定成功
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 银行处理时间 格式：yyyyMMddhh24mmss
        /// </summary>
        public string BankTxTime { get; set; }

        /// <summary>
        /// 响应代码
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// 实际发卡银行 ID，参考《银行编码表》(如果库中 IssueBankID 值为 700 直接给空)
        /// </summary>
        public string IssueBankID { get; set; }

        /// <summary>
        /// 卡类型：10=个人借记 20=个人贷记 (如果库中 IssueBankID 值为 700 直接给空)
        /// </summary>
        public string IssueCardType { get; set; }

        /// <summary>
        /// 发卡机构代码（银联返回，不是Request 中的 BankID）
        /// </summary>
        public string IssInsCode { get; set; }

        /// <summary>
        /// 支付卡类型（银联返回，不是 Request中的 CardType） 00=未知 01=借记账户 02=贷记账户 03=准贷记账户 04=借贷合一账户 05=预付费账户 06=半开放预付费账户
        /// </summary>
        public string PayCardType { get; set; }
    }
}
