using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PBI.APP.API
{
    public class AES
    {


        public static string EncryptDES_ECB_Base64(string pToEncrypt, string sKey)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(sKey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(pToEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //rDel.IV = null;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptDES_ECB_Base64(string DecryptStr, string Key)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                // byte[] keyArray = Convert.FromBase64String(Key);
                byte[] toEncryptArray = Convert.FromBase64String(DecryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {


                return ex.Message;
            }
        }
    }


}
