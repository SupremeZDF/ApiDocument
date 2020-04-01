using System;
using System.Collections.Generic;
using System.Text;
using CFCA.Payment.Api;
using CFCA.Payment;
using System.Xml;

namespace ZJ_Interface
{
    public class Tx2501Response : ZJPayMent.Tool.TxBaseResponse
    {
        private String institutionID;
        private String paymentNo;
        private long amount;
        private String remark;
        private int status;
        private String bankNotificationTime;
        public Tx2501Response(String responseMessage, String responseSignature)
        : base(responseMessage, responseSignature)
        {

        }
        protected override void process(XmlDocument document)
        {
            if ("2000".Equals(code))
            {
                this.institutionID = XmlUtil.getNodeText(document, "InstitutionID");
                this.paymentNo = XmlUtil.getNodeText(document, "PaymentNo");
                this.amount = Convert.ToInt64(XmlUtil.getNodeText(document,
               "Amount"));
                this.remark = XmlUtil.getNodeText(document, "Remark");
                this.status = Convert.ToInt32(XmlUtil.getNodeText(document,
               "Status"));
                this.bankNotificationTime = XmlUtil.getNodeText(document,
               "BankNotificationTime");
            }
        }
        public String getInstitutionID()
        {
            return institutionID;
        }
        public String getPaymentNo()
        {
            return paymentNo;
        }
        public long getAmount()
        {
            return amount;
        }
        public int getStatus()
        {
            return status;
        }
        public String getRemark()
        {
            return remark;
        }
        public String getBankNotificationTime()
        {
            return bankNotificationTime;
        }

    }
}
