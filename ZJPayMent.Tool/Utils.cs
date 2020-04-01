using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ZJPayMent.Tool
{
    public class Utils
    {
		public static string Base64UrlEncoding(string str)
		{
			return str.Replace("+", "%2B").Replace("/", "%2F");
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public static byte[] hex2bytes(string hexString)
		{
			hexString = hexString.ToUpper();
			char[] array = hexString.ToCharArray();
			byte[] array2 = new byte[array.Length / 2];
			int num = 0;
			for (int i = 0; i < array.Length; i += 2)
			{
				byte b = 0;
				b |= Utils.char2byte(array[i]);
				b = (byte)(b << 4);
				b |= Utils.char2byte(array[i + 1]);
				array2[num] = b;
				num++;
			}
			return array2;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001D96C File Offset: 0x0001BB6C
		public static byte char2byte(char ch)
		{
			switch (ch)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case 'A':
					return 10;
				case 'B':
					return 11;
				case 'C':
					return 12;
				case 'D':
					return 13;
				case 'E':
					return 14;
				case 'F':
					return 15;
			}
			return 0;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public static byte[] Hash(string text)
		{
			SHA1 sha = SHA1.Create();
			return sha.ComputeHash(Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		public static string ConvertBytesToHexString(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		public static string CreateNumberByTime()
		{
			DateTime now = DateTime.Now;
			return string.Format("{0:yyyyMMddHHmmssfff}", now) + Utils.RandCode(10);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public static string CreateNumberByTime20()
		{
			DateTime now = DateTime.Now;
			return string.Format("{0:yyyyMMddHHmmssfff}", now) + Utils.RandCode(3);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001DB34 File Offset: 0x0001BD34
		public static string RandCode(int N)
		{
			char[] array = new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			};
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random(Guid.NewGuid().GetHashCode());
			for (int i = 0; i < N; i++)
			{
				stringBuilder.Append(array[random.Next(0, array.Length)].ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
