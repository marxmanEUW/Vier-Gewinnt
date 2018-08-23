using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VierGewinntClient
{
    class Cryptography
    {

        #region Variables

        private int[] _AllowedUTFChars =
        {
                32, // Space
                33, // !
                34, // "
                35, // #
                36, // $
                37, // %
                38, // &
                39, // '
                44, // ,
                46, // .
                65, // A
                66, // B
                67, // C
                68, // D
                69, // E
                70, // F
                71, // G
                72, // H
                73, // I
                74, // J
                75, // K
                76, // L
                77, // M
                78, // N
                79, // O
                80, // P
                81, // Q
                82, // R
                83, // S
                84, // T
                85, // U
                86, // V
                87, // W
                88, // X
                89, // Y
                90, // Z
                91, // [
                93, // ]
                97, // a
                98, // b
                99, // c
                100, // d
                101, // e
                102, // f
                103, // g
                104, // h
                105, // i
                106, // j
                107, // k
                108, // l
                109, // m
                110, // n
                111, // o
                112, // p
                113, // q
                114, // r
                115, // s
                116, // t
                117, // u
                118, // v
                119, // w
                120, // x
                121, // y
                122, // z
                123, // {
                125, // }
                132, // Ä
                150, // Ö
                156, // Ü
                164, // ä
                182, // ö
                188, // ü
                195, // extended  
        };

        private int _AesKeyLength = 32; // in byte
        private int _AesBlockLength = 16;

        private int _RsaKeyLength = 2048; // in bit

        private static RSAParameters _PrivateKey;
        public static RSAParameters PublicKey { get; set; }

        #endregion

        #region Helper Methods

        private byte[] DoExtendKey(string aKey, int aNewKeyLength)
        {
            byte[] bKey = new byte[aNewKeyLength];
            byte[] tmpKey = Encoding.UTF8.GetBytes(aKey);

            for (int i = 0; i < aKey.Length; i++)
            {
                bKey[i] = tmpKey[i];
            }

            return bKey;
        }

        private byte[] DoCreateBlocksize(int aNewBlockSize)
        {
            byte[] block = new byte[aNewBlockSize];

            for (byte i = 0; i < aNewBlockSize; i++)
            {
                block[i] = i;
            }

            return block;
        }

        public string GetKeyString(RSAParameters aKEy)
        {
            StringWriter stringWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, aKEy);
            return stringWriter.ToString();
        }

        #endregion

        #region Crypto Methods

        private string AesEncrypt(string aPlainText, string aKey)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Key = DoExtendKey(aKey, _AesKeyLength);
            AESCrypto.IV = DoCreateBlocksize(_AesBlockLength);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, AESCrypto.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.UTF8.GetBytes(aPlainText);
            cStream.Write(plainBytes, 0, plainBytes.Length);
            cStream.FlushFinalBlock();

            byte[] encryptedBytes = mStream.ToArray();
            string encryptedString = Convert.ToBase64String(encryptedBytes);

            mStream.Close();
            cStream.Close();

            return encryptedString;
        }

        private string AesDecrypt(string aEncryptedText, string aKey)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Padding = PaddingMode.Zeros;
            AESCrypto.Key = DoExtendKey(aKey, _AesKeyLength);
            AESCrypto.IV = DoCreateBlocksize(_AesBlockLength);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, AESCrypto.CreateDecryptor(), CryptoStreamMode.Write);

            string plainText = String.Empty;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(aEncryptedText);
                cStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                cStream.FlushFinalBlock();
            }
            catch
            {
                MessageBox.Show("Fehlerhafte Chiffre!", "", MessageBoxButtons.OK);
                cStream.FlushFinalBlock();
            }
            finally
            {
                mStream.Close();
                cStream.Close();
            }

            byte[] plainBytes = mStream.ToArray();

            // remove extra bytes, that were added from Aes encryption (Blockchiffre)
            byte[] onlyChars = new byte[plainBytes.Length];

            for (int i = 0; i < plainBytes.Length; i++)
            {
                if (_AllowedUTFChars.Contains(plainBytes[i]))
                {
                    onlyChars[i] = plainBytes[i];
                }
            }

            plainText = Encoding.UTF8.GetString(onlyChars);

            return plainText;
        }
        
        public string RsaEncrypt(string aPlainText, RSAParameters aPublicKey)
        {
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(aPlainText);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_RsaKeyLength))
            {
                try
                {
                    rsa.ImportParameters(aPublicKey);

                    byte[] encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    string base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public string RsaDecrypt(string aEncryptedText, RSAParameters aPrivateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(_RsaKeyLength))
            {
                try
                {
                    rsa.ImportParameters(aPrivateKey);

                    byte[] resultBytes = Convert.FromBase64String(aEncryptedText);
                    byte[] decryptedBytes = rsa.Decrypt(resultBytes, true);
                    string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        #endregion
    }
}
