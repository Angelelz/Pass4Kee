using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using System.IO;

namespace Pass4Kee.Utils
{
    public class Library
    {
        

        private const BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
        private const string strDescriptor = "LOCAL=user";

        private const string FILE_NAME = "MyEncryptedPass.txt";
        private static string _passwordPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, FILE_NAME);

        public static async void SavePassToFile(string MyEncriptedPass)
        {
            if (File.Exists(_passwordPath))
            {
                StorageFile PassFile = await StorageFile.GetFileFromPathAsync(_passwordPath);
                await FileIO.WriteTextAsync(PassFile, MyEncriptedPass);
            }
            else
            {
                StorageFile PassFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FILE_NAME);
                await FileIO.WriteTextAsync(PassFile, MyEncriptedPass);
            }
        }

        public static async Task<bool> IsFileEmpty()
        {
            if (File.Exists(_passwordPath))
            {
                StorageFile PassFile = await StorageFile.GetFileFromPathAsync(_passwordPath);

                string MyEncriptedPass = await FileIO.ReadTextAsync(PassFile);
                if (MyEncriptedPass == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static async Task<string> ReadPassFromFile()
        {
            if (File.Exists(_passwordPath))
            {
                StorageFile PassFile = await StorageFile.GetFileFromPathAsync(_passwordPath);

                string MyEncriptedPass = await FileIO.ReadTextAsync(PassFile);
                return MyEncriptedPass;
            }

            return "";
        }



        
        
        public static async Task<string> Encrypt(string strClearText)
        
        {
            DataProtectionProvider Provider = new DataProtectionProvider(strDescriptor);
        
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strClearText, encoding);
        
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);
        
            return CryptographicBuffer.EncodeToBase64String(buffProtected);
        }
        
        public static async Task<String> Decrypt(string strProtected)
        {
            DataProtectionProvider Provider = new DataProtectionProvider();
        
            IBuffer buffProtected = CryptographicBuffer.DecodeFromBase64String(strProtected);
        
            IBuffer buffUnprotected = await Provider.UnprotectAsync(buffProtected);
        
            String strClearText = CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotected);
        
            return strClearText;
        }
        


    }
}
