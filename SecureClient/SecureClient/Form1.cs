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

namespace SecureClient
{
    public partial class Form1 : Form
    {
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

        private string AesDecrypt(string plainText, string key)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Key = DoExtendKey(key, 32);
            AESCrypto.IV = DoCreateBlocksize(16);

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

        private string AesEncrypt(string encryotedText, string key)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Padding = PaddingMode.Zeros;
            AESCrypto.Key = DoExtendKey(key, 32);
            AESCrypto.IV = DoCreateBlocksize(16);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, AESCrypto.CreateDecryptor(), CryptoStreamMode.Write);

            string plainText = "";

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryotedText);
                cStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                cStream.FlushFinalBlock();

                byte[] plainBytes = mStream.ToArray();
                plainText = Encoding.UTF8.GetString(plainBytes);
            }
            catch
            {
                MessageBox.Show("Fehlerhafte Chiffre!", "", MessageBoxButtons.OK);
                cStream.FlushFinalBlock();
            }

            mStream.Close();
            cStream.Close();

            return plainText;
        }

        private void VerschluesselnButton_Click(object sender, EventArgs e)
        {
            ChiffreTextbox.Text = AesDecrypt(KlartextTextbox1.Text, SchluesselTextbox.Text);
        }

        private void EntschluesselnButton_Click(object sender, EventArgs e)
        {
            KlartextTextbox2.Text = AesEncrypt(ChiffreTextbox.Text, SchluesselTextbox.Text);
        }
    }
}
