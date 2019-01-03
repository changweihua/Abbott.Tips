using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public sealed class EncryptUtil
    {

        #region DES加密解密字符串

        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥，要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回null</returns>
        public static string EncryptDES(string encryptString, string encryptKey = "11001100")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥，要求为8位，和加密密钥相同</param>
        /// <returns>解密成功后返回解密后的字符串，失败返回null</returns>
        public static string DecryptDES(string decryptString, string decryptKey = "11001100")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region 兼容 Java 的 DES加解密  废弃

        /// <summary> 
        /// DES加密 
        /// </summary> 
        /// <param name="encryptString"></param> 
        /// <returns></returns> 
        [Obsolete]
        public static string CompatiableEncryptDES(string strEncryptString, string encryptKey = "11001100")
        {
            StringBuilder strRetValue = new StringBuilder();

            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strEncryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                //不使用base64编码
                //return Convert.ToBase64String(mStream.ToArray()); 

                //组织成16进制字符串            
                foreach (byte b in mStream.ToArray())
                {
                    strRetValue.AppendFormat("{0:X2}", b);
                }
            }
            catch (Exception e)
            {
            }

            return strRetValue.ToString();
        }

        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="decryptString"></param> 
        /// <returns></returns>         
        [Obsolete]
        public static string CompatiableDecryptDES(string strDecryptString, string encryptKey = "11001100")
        {
            string strRetValue = "";

            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] keyIV = keyBytes;

                //不使用base64解码
                //byte[] inputByteArray = Convert.FromBase64String(decryptString);

                //16进制转换为byte字节
                byte[] inputByteArray = new byte[strDecryptString.Length / 2];
                for (int x = 0; x < strDecryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(strDecryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0  

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                //需要去掉结尾的null字符
                //strRetValue = Encoding.UTF8.GetString(mStream.ToArray());
                strRetValue = Encoding.UTF8.GetString(mStream.ToArray()).TrimEnd('\0');
            }
            catch (Exception e)
            {
            }

            return strRetValue;
        }

        #endregion

        #region 兼容 Java 的 DES加解密

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DesEncrypt(string sourceString, string sKey = "11001100")
        {
            byte[] btKey = Encoding.UTF8.GetBytes(sKey);
            byte[] btIV = Keys;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位</param>
        /// <returns>已解密的字符串</returns>
        public static string DesDecrypt(string pToDecrypt, string sKey = "11001100")
        {
            //转义特殊字符
            pToDecrypt = pToDecrypt.Replace("-", "+");
            pToDecrypt = pToDecrypt.Replace("_", "/");
            pToDecrypt = pToDecrypt.Replace("~", "=");
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = Keys;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        #endregion

        /// <summary>
        /// MD5 16位加密 加密后代码为大写
        /// </summary>
        /// <param name="cryptString">待加密的字符串</param>
        /// <returns>密文</returns>
        public static string GetMD5StringUpperCase(string cryptString)
        {
            string result = string.Empty;

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(cryptString)), 4, 8);
                result = result.Replace("-", "");
            }

            return result;
        }

        /// <summary>
        /// MD5 16位加密 加密后代码为小写
        /// </summary>
        /// <param name="cryptString">待加密的字符串</param>
        /// <returns>密文</returns>
        public static string GetMD5StringLowerCase(string cryptString)
        {
            return GetMD5StringUpperCase(cryptString).ToLower();
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="cryptString">明文</param>
        /// <returns>密文</returns>
        public static string GetMD5String(string cryptString)
        {
            string result = string.Empty;

            //实例化一个MD5对象
            using (MD5 md5 = MD5.Create())
            {
                StringBuilder sb = new StringBuilder();
                //加密后是一个字节类型的数组
                byte[] cryptStringArray = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptString));
                //通过循环，将字节类型的数组转换成字符串
                for (int i = 0; i < cryptStringArray.Length; i++)
                {
                    //将得到的字符串使用16进制类型格式
                    sb.Append(cryptStringArray[i].ToString("X"));
                }

                result = sb.ToString();

            }

            return result;
        }


        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("X").PadLeft(2, '0'));
            }

            return sb.ToString();
        }


        /// <summary>
        /// 加密类型
        /// </summary>
        [Flags]
        public enum EncryptType
        {
            /// <summary>
            /// MD5加密，不可逆
            /// </summary>
            MD5,
            /// <summary>
            /// DES加密
            /// </summary>
            DES,
            /// <summary>
            /// AES加密
            /// </summary>
            AES
        }

    }
}
