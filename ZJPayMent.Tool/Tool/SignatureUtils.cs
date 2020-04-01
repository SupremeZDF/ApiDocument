using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using BT.Manage.Frame.Base;
using System.Text;

namespace ZJPayMent.Tool
{
    public class SignatureUtils
    {
		public static void InitSign(string keystoreFile, string keystorePass)
		{
			try
			{
				RSACryptoServiceProvider key = new RSACryptoServiceProvider();
				SignatureUtils.rsaPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
				X509Certificate2 x509Certificate = new X509Certificate2(keystoreFile, keystorePass, X509KeyStorageFlags.MachineKeySet);
				SignatureUtils.rsaPKCS1SignatureFormatter.SetKey(x509Certificate.PrivateKey);
				SignatureUtils.rsaPKCS1SignatureFormatter.SetHashAlgorithm("SHA1");
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			catch (SystemException ex2)
			{
				throw ex2;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public static void InitVerify(string cert)
		{
			try
			{
				X509Certificate2 x509Certificate = new X509Certificate2(cert);
				string xmlString = x509Certificate.PublicKey.Key.ToXmlString(false);
				RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
				rsacryptoServiceProvider.FromXmlString(xmlString);
				SignatureUtils.rsaPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsacryptoServiceProvider);
				SignatureUtils.rsaPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			catch (SystemException ex2)
			{
				throw ex2;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public static string Sign(string data)
		{
			string result;
			try
			{
				byte[] rgbHash = Utils.Hash(data);
				InitVerify("D:/学习/练习Excitens/UploadDocument/ApiDocument/ZJPayMentAspNerCore/config/paytest.cer");
				InitSign("D:/学习/练习Excitens/UploadDocument/ApiDocument/ZJPayMentAspNerCore/config/test.pfx", "cfca1234");
				byte[] bytes = SignatureUtils.rsaPKCS1SignatureFormatter.CreateSignature(rgbHash);
				string text = Utils.ConvertBytesToHexString(bytes);
				result = text;
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			catch (SystemException ex2)
			{
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public static bool Verify(string text, byte[] signature)
		{
			bool result;
			try
			{
				byte[] rgbHash = Utils.Hash(text);
				bool flag = SignatureUtils.rsaPKCS1SignatureDeformatter.VerifySignature(rgbHash, signature);
				if (flag)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
			catch (SystemException ex2)
			{
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public static bool Verify(string text, string signature)
		{
			return SignatureUtils.Verify(text, Utils.hex2bytes(signature));
		}

		// Token: 0x04000233 RID: 563
		private static RSAPKCS1SignatureFormatter rsaPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter();

		// Token: 0x04000234 RID: 564
		private static RSAPKCS1SignatureDeformatter rsaPKCS1SignatureDeformatter=new RSAPKCS1SignatureDeformatter();
	}
}
