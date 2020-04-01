using BT.Manage.Frame.Base;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ZJPayMent.Tool
{
    public abstract class TxBaseRequest
    {
		public string getRequestPlainText()
		{
			return this.requestPlainText;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002490 File Offset: 0x00000690
		public string getRequestMessage()
		{
			return this.requestMessage;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024A8 File Offset: 0x000006A8
		public string getRequestSignature()
		{
			return this.requestSignature;
		}

		// Token: 0x0600001D RID: 29
		public abstract void process();

		// Token: 0x0600001E RID: 30 RVA: 0x000024C0 File Offset: 0x000006C0
		protected void postProcess(XmlDocument document)
		{
			Result result=new Result();
			this.requestPlainText = XmlUtil.XmlDocument2XmlString(document);
			this.requestMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(this.requestPlainText));
			this.requestSignature = SignatureUtils.Sign(this.requestPlainText);
			
			//result.message = resObj.message;
			//result.@object = null;
			//result.code = 0;
		
		}

		// Token: 0x04000017 RID: 23
		protected string txCode;

		// Token: 0x04000018 RID: 24
		protected string requestPlainText;

		// Token: 0x04000019 RID: 25
		protected string requestMessage;

		// Token: 0x0400001A RID: 26
		protected string requestSignature;
	}
}
