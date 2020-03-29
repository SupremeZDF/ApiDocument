using System;
using System.Collections.Generic;
using System.Text;
using ZJPayMent.DAL;
using CFCA.Payment.Api;
using System.Xml;

namespace ZJ_Interface
{
    public class ComeTrue2501Class : TxBaseRequest, InterFace2501
    {

        private String institutionID;
        private String paymentNo;
        private Class2501Request Class2501Request { get; set; }

        public ComeTrue2501Class()
        {
            this.txCode = "2501";
        }

        public override void process()
        {
            // 组装报文
            XmlDocument document = new XmlDocument();
            XmlNode xmlNode = document.CreateNode(XmlNodeType.XmlDeclaration, "","");
            document.AppendChild(xmlNode);
            // 创建节点
            // 创建主节点
            XmlElement Request = document.CreateElement("", "Request", "");
            XmlElement Head = document.CreateElement("Head");
            XmlElement Body = document.CreateElement("Body");

            //创建数据节点
            XmlElement TxCode = document.CreateElement("TxCode");
            TxCode.InnerText = txCode;

            XmlElement InstitutionID = document.CreateElement("InstitutionID");
            InstitutionID.InnerText = ReadJsonConfig.GetConfig().GetSettingNode("InstitutionID");
            //XmlElement PaymentNo = document.CreateElement("PaymentNo");

            XmlElement TxSNBinding = document.CreateElement("TxSNBinding");
            TxSNBinding.InnerText = Class2501Request.TxSNBinding;

            XmlElement BankID = document.CreateElement("BankID");
            BankID.InnerText = Class2501Request.BankID;

            XmlElement AccountName = document.CreateElement("AccountName");
            AccountName.InnerText = Class2501Request.AccountName;

            XmlElement AccountNumber = document.CreateElement("AccountNumber");
            AccountNumber.InnerText = Class2501Request.AccountNumber;

            XmlElement IdentificationType = document.CreateElement("IdentificationType");
            IdentificationType.InnerText = Class2501Request.IdentificationType;

            XmlElement IdentificationNumber = document.CreateElement("IdentificationNumber");
            IdentificationNumber.InnerText = Class2501Request.IdentificationNumber;

            XmlElement PhoneNumber = document.CreateElement("PhoneNumber");
            PhoneNumber.InnerText = Class2501Request.PhoneNumber;

            XmlElement CardType = document.CreateElement("CardType");
            CardType.InnerText = Class2501Request.CardType;

            XmlElement ValidDate = document.CreateElement("ValidDate");
            ValidDate.InnerText = Class2501Request.ValidDate;

            XmlElement CVN2 = document.CreateElement("CVN2");
            CVN2.InnerText = Class2501Request.CVN2;

            // 组装并赋值
            //Request.SetAttribute("version", "", "2.1");
            document.AppendChild(Request);
            Request.AppendChild(Head);
            Request.AppendChild(Body);
            Head.AppendChild(TxCode);
            //TxCode.AppendChild(document.CreateTextNode(this.txCode));
            Head.AppendChild(InstitutionID);
            Body.AppendChild(TxSNBinding);
            Body.AppendChild(BankID);
            Body.AppendChild(AccountName);
            Body.AppendChild(AccountNumber);
            Body.AppendChild(IdentificationType);
            Body.AppendChild(IdentificationNumber);
            Body.AppendChild(PhoneNumber);
            Body.AppendChild(CardType);
            Body.AppendChild(ValidDate);
            Body.AppendChild(CVN2);
            //InstitutionID.AppendChild(document.CreateTextNode(this.institutionID));
            //Body.AppendChild(PaymentNo);
            //PaymentNo.AppendChild(document.CreateTextNode(this.paymentNo));
            // 产生交易发送所需数据 
            postProcess(document);
        }


        /// <summary>
        /// 建立绑定关系
        /// </summary>
        /// <param name="Prameter2501Request"></param>
        /// <returns></returns>
        public Class2501Response Ruquest2501Method(Class2501Request Prameter2501Request)
        {
            this.Class2501Request = Prameter2501Request;
            process();
            return null;
        }
    }
}
