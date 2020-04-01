using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ZJPayMent.Tool
{
    public abstract class TxBaseResponse
    {
		public TxBaseResponse(string responseMessage, string responseSignature)
		{
			this.responseMessage = responseMessage.Trim();
			this.responseSignature = responseSignature.Trim();
			this.responsePlainText = Encoding.UTF8.GetString(Convert.FromBase64String(responseMessage));
			if (!SignatureUtils.Verify(this.responsePlainText, this.responseSignature) && !SignatureUtils.Verify(this.responseMessage, this.responseSignature))
			{
				string text = "验证签名失败。";
				throw new Exception(text);
			}
			XmlDocument xmlDocument = XmlUtil.XmlString2XmlDocument(this.responsePlainText);
			this.code = XmlUtil.getNodeText(xmlDocument, "Code");
			this.message = XmlUtil.getNodeText(xmlDocument, "Message");
			this.process(xmlDocument);
		}

		// Token: 0x06000008 RID: 8
		protected abstract void process(XmlDocument document);

		// Token: 0x06000009 RID: 9 RVA: 0x000021F4 File Offset: 0x000003F4
		public string getCode()
		{
			return this.code;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220C File Offset: 0x0000040C
		public string getMessage()
		{
			return this.message;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		public string getResponsePlainText()
		{
			return this.responsePlainText;
		}

		// Token: 0x04000006 RID: 6
		protected string responseMessage;

		// Token: 0x04000007 RID: 7
		protected string responseSignature;

		// Token: 0x04000008 RID: 8
		protected string responsePlainText;

		// Token: 0x04000009 RID: 9
		protected string code;

		// Token: 0x0400000A RID: 10
		protected string message;
	}
}
