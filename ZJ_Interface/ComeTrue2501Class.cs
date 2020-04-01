using System;
using System.Collections.Generic;
using System.Text;
using ZJPayMent.DAL;
using CFCA.Payment.Api;
using CFCA.Payment;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using ZJPayMent.Tool;
using BT.Manage.Frame.Base;
using RestSharp;

namespace ZJ_Interface
{
    public class ComeTrue2501Class : ZJPayMent.Tool.TxBaseRequest, InterFace2501
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
            AccountNumber.InnerText = Class2501Request.AccountNumber;  //ss

            XmlElement IdentificationType = document.CreateElement("IdentificationType");
            IdentificationType.InnerText = Class2501Request.IdentificationType;

            XmlElement IdentificationNumber = document.CreateElement("IdentificationNumber");
            IdentificationNumber.InnerText = Class2501Request.IdentificationNumber;

            XmlElement PhoneNumber = document.CreateElement("PhoneNumber");
            PhoneNumber.InnerText = Class2501Request.PhoneNumber;

            XmlElement CardType = document.CreateElement("CardType");
            CardType.InnerText = Class2501Request.CardType;

            //XmlElement ValidDate = document.CreateElement("ValidDate");
            //ValidDate.InnerText = Class2501Request.ValidDate;

            //XmlElement CVN2 = document.CreateElement("CVN2");
            //CVN2.InnerText = Class2501Request.CVN2;

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
            //Body.AppendChild(ValidDate);
            //Body.AppendChild(CVN2);
            //InstitutionID.AppendChild(document.CreateTextNode(this.institutionID));
            //Body.AppendChild(PaymentNo);
            //PaymentNo.AppendChild(document.CreateTextNode(this.paymentNo));
            // 产生交易发送所需数据 
            //string cert = "D:/学习/练习Excitens/UploadDocument/ApiDocument/ZJPayMentAspNerCore/config/paytest.cer";
            //SignatureUtils.InitVerify(cert);

            //try
            //{
            //    X509Certificate2 x509Certificate = new X509Certificate2(cert);
            //    string xmlString = x509Certificate.PublicKey.Key.ToXmlString(false);
            //    RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
             
            //}
            //catch (CryptographicException ex)
            //{
            //    throw ex;
            //}
            //catch (SystemException ex2)
            //{
            //    throw ex2;
            //}

            postProcess(document);

            BaseRequest baseRequest = new BaseRequest();
            RestRequest rest = new RestRequest(Method.POST);
            rest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            rest.AddParameter("message", this.requestMessage);
            rest.AddParameter("signature", this.requestSignature);
            //rest.AddJsonBody(new
            //{
            //    message = this.requestMessage,
            //    signature = this.requestSignature
            //});
            var ResResponse = baseRequest.SyncRequestByResourcePath(rest, null, "https://test.cpcn.com.cn/Gateway/InterfaceII");
            string mesAndsing = ResResponse.@object;
            string[] s = mesAndsing.Split(',');

            Tx2501Response tx2501 = new Tx2501Response(s[0],s[1]);
            //var resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditResult>(ResResponse.@object);
            if (ResResponse.code == 1)
            {
                //result.code = 1;
                //result.@object = resObj.@object;

            }
        }


        /// <summarys
        /// 建立绑定关系
        /// </summary>
        /// <param name="Prameter2501Request"></param>
        /// <returns></returns>Object reference not set to an instance of an object
        public Class2501Response Ruquest2501Method(Class2501Request Prameter2501Request)
        {
            this.Class2501Request = Prameter2501Request;
            process();
            return null;
        }

        public string Response2501Method()
        {
            return "1";
        }
    }
}
