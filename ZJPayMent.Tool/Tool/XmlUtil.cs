using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ZJPayMent.Tool
{
    public class XmlUtil
    {
		// Token: 0x0600070A RID: 1802 RVA: 0x00015408 File Offset: 0x00013608
		public static string XmlDocument2XmlString(XmlDocument xmlDocument)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlDocument.WriteTo(xmlTextWriter);
			string result = stringWriter.ToString();
			xmlTextWriter.Close();
			return result;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00015440 File Offset: 0x00013640
		public static XmlDocument XmlString2XmlDocument(string xmlString)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			MemoryStream inStream = new MemoryStream(new UTF8Encoding().GetBytes(xmlString));
			xmlDocument.Load(inStream);
			return xmlDocument;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001547C File Offset: 0x0001367C
		public static string getNodeText(XmlDocument doc, string tagName)
		{
			XmlNodeList elementsByTagName = doc.GetElementsByTagName(tagName);
			string result;
			if (elementsByTagName != null && elementsByTagName[0] != null)
			{
				result = elementsByTagName[0].InnerXml;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000154C0 File Offset: 0x000136C0
		public static string getChildNodeText(XmlNode node, string childName)
		{
			XmlNodeList childNodes = node.ChildNodes;
			int count = childNodes.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = childNodes.Item(i);
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == childName)
				{
					return xmlNode.InnerXml;
				}
			}
			return "";
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00015530 File Offset: 0x00013730
		public static string formatXmlString(string xmlString)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			XmlUtil.XmlString2XmlDocument(xmlString).WriteTo(xmlTextWriter);
			string result = stringWriter.ToString();
			xmlTextWriter.Close();
			return result;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00015574 File Offset: 0x00013774
		public static XmlNode getChildNodeByName(XmlNode parentNode, string nodeName)
		{
			for (XmlNode xmlNode = parentNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (nodeName.Equals(xmlNode.Name))
				{
					return xmlNode;
				}
			}
			return null;
		}
	}
}
