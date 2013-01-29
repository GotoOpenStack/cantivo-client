using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
namespace Adell.Convenience.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64String(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string ToMd5String(this string str)
        {
            byte[] origBytes = ASCIIEncoding.Default.GetBytes(str);
            byte[] encBytes = new MD5CryptoServiceProvider().ComputeHash(origBytes);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < encBytes.Length; i++)
            {
                result.Append(encBytes[i].ToString("x2"));
            }

            return result.ToString();
        }

        public static String String(this SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}