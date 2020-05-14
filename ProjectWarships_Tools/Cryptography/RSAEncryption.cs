using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWarships_Tools.Cryptography
{
    public class RSAEncryption : IRSAEncryption
    {
        private readonly RSACryptoServiceProvider _rsaCryptoServiceProvider;
        private readonly UnicodeEncoding _unicodeEncoding;
        public int KeySize
        {
            get { return _rsaCryptoServiceProvider.KeySize; }
        }

        public byte[] BinaryKeys
        {
            get { return _rsaCryptoServiceProvider.ExportCspBlob(true); }
        }

        public byte[] PublicBinaryKey
        {
            get { return _rsaCryptoServiceProvider.ExportCspBlob(false); }
        }

        public string XmlKeys
        {
            get { return _rsaCryptoServiceProvider.ToXmlString(true); }
        }

        public string PublicXmlKey
        {
            get
            {
                return _rsaCryptoServiceProvider.ToXmlString(false);
            }
        }

        public RSAEncryption()
        {
            _rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            _rsaCryptoServiceProvider.KeySize = 2048;
            _unicodeEncoding = new UnicodeEncoding();
        }

        public RSAEncryption(int keySize)
        {
            _rsaCryptoServiceProvider = new RSACryptoServiceProvider(keySize);
            _unicodeEncoding = new UnicodeEncoding();
        }

        public RSAEncryption(byte[] keyInfo)
        {
            _rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            _rsaCryptoServiceProvider.ImportCspBlob(keyInfo);
            _unicodeEncoding = new UnicodeEncoding();
        }

        public RSAEncryption(string keyInfo)
        {
            _rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            _rsaCryptoServiceProvider.FromXmlString(keyInfo);
            _unicodeEncoding = new UnicodeEncoding();
        }

        public byte[] Encrypt(string value)
        {
            byte[] toEncrypt = _unicodeEncoding.GetBytes(value);
            return _rsaCryptoServiceProvider.Encrypt(toEncrypt, true);
        }

        public string Decrypt(byte[] value)
        {
            byte[] binaryValue = _rsaCryptoServiceProvider.Decrypt(value, true);
            return _unicodeEncoding.GetString(binaryValue);
        }

        public void ImportBinaryKeys(byte[] keys)
        {
            _rsaCryptoServiceProvider.ImportCspBlob(keys);
        }

        public void ImportXmlKeys(string xml)
        {
            _rsaCryptoServiceProvider.FromXmlString(xml);
        }
    }
}
