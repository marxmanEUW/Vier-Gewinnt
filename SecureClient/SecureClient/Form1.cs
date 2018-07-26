using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SecureClient
{
    public partial class Form1 : Form
    {
        int[] AllowedUTFChars =
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

        int AesKeyLength = 32; // in byte
        int AesBlockLength = 16;

        int RsaKeyLength = 2048; // in bit

        RSAParameters privateKey;
        RSAParameters publicKey;

        public Form1()
        {
            InitializeComponent();
        }

        private byte[] DoExtendKey(string key, int newKeyLength)
        {
            byte[] bKey = new byte[newKeyLength];
            byte[] tmpKey = Encoding.UTF8.GetBytes(key);

            for (int i = 0; i < key.Length; i++)
            {
                bKey[i] = tmpKey[i];
            }

            return bKey;
        }

        private byte[] DoCreateBlocksize(int newBlocksize)
        {
            byte[] block = new byte[newBlocksize];

            for (byte i = 0; i < newBlocksize; i++)
            {
                block[i] = i;
            }

            return block;
        }

        private string AesEncrypt(string plainText, string key)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Key = DoExtendKey(key, AesKeyLength);
            AESCrypto.IV = DoCreateBlocksize(AesBlockLength);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, AESCrypto.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            cStream.Write(plainBytes, 0, plainBytes.Length);
            cStream.FlushFinalBlock();

            byte[] encryptedBytes = mStream.ToArray();
            string encryptedString = Convert.ToBase64String(encryptedBytes);

            mStream.Close();
            cStream.Close();

            return encryptedString;
        }

        private string AesDecrypt(string encryptedText, string key)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Padding = PaddingMode.Zeros;
            AESCrypto.Key = DoExtendKey(key, AesKeyLength);
            AESCrypto.IV = DoCreateBlocksize(AesBlockLength);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, AESCrypto.CreateDecryptor(), CryptoStreamMode.Write);

            string plainText = "";

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                cStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                cStream.FlushFinalBlock();
            }
            catch
            {
                MessageBox.Show("Fehlerhafte Chiffre!", "", MessageBoxButtons.OK);
                cStream.FlushFinalBlock();
            }

            mStream.Close();
            cStream.Close();

            byte[] plainBytes = mStream.ToArray();

            // remove extra bytes, that were added from Aes encryption (Blockchiffre)
            byte[] onlyChars = new byte[plainBytes.Length];

            for( int i = 0; i < plainBytes.Length; i++)
            {
                if (AllowedUTFChars.Contains(plainBytes[i]))
                {
                    onlyChars[i] = plainBytes[i];
                }
            }

            plainText = Encoding.UTF8.GetString(onlyChars);
            
            return plainText;
        }

        public string GetKeyString(RSAParameters key)
        {
            StringWriter stringWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, key);
            return stringWriter.ToString();
        }

        public string RsaEncrypt(string plainText, RSAParameters publicKey)
        {
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(plainText);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RsaKeyLength))
            {
                try
                {
                    rsa.ImportParameters(publicKey);

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

        public string RsaDecrypt(string encryptedText, RSAParameters privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(RsaKeyLength))
            {
                try
                {
                    rsa.ImportParameters(privateKey);

                    byte[] resultBytes = Convert.FromBase64String(encryptedText);
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

        private void VerschluesselnButton_Click(object sender, EventArgs e)
        {
            //ChiffreTextbox.Text = AesEncrypt(KlartextTextbox1.Text, SchluesselTextbox.Text);
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(RsaKeyLength);
            privateKey = cryptoServiceProvider.ExportParameters(true);
            publicKey = cryptoServiceProvider.ExportParameters(false);

            string publicKeyString = GetKeyString(publicKey);
            string privateKeyString = GetKeyString(privateKey);
            SchluesselTextbox.Text = publicKeyString;

            ChiffreTextbox.Text = RsaEncrypt(KlartextTextbox1.Text, publicKey);
        }

        private void EntschluesselnButton_Click(object sender, EventArgs e)
        {
            //KlartextTextbox2.Text = AesDecrypt(ChiffreTextbox.Text, SchluesselTextbox.Text);
            KlartextTextbox2.Text = RsaDecrypt(ChiffreTextbox.Text, privateKey);
        }
    }
}
