using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypter_Base
{
    public class Program
    {
        private static string strRoot = "C:\\Dokumente";
        private static string strEncryptedFolderName = "EncryptedFiles";
        private static string strDecryptedFolderName = "DecryptedFiles";
        private static string strEncryptedFileName = "Encrypted_";
        private static string strDecryptedFileName = "Decrypted_";

        private static string strEncryptedFolderPath = strRoot + "\\" + strEncryptedFolderName + "\\";
        private static string strDecryptedFolderPath = strRoot + "\\" + strDecryptedFolderName + "\\";
        private static string strEncryptedFilePath = null;
        private static string strDecryptedFilePath = null;
        private static string strFilePath = null;
        private static string strFileName = null;

        private static string strKey = "1234512345678976";


        static void Main(string[] args)
        {
            TraverseFileSystem(strRoot);
            Console.ReadLine();
        }

        public static void TraverseFileSystem(String strDir)
        {
            try
            {
                CheckIfFolderExists();

                foreach (String stringFilePath in Directory.GetFiles(strDir))
                {
                    strFilePath = stringFilePath;

                    SetFileNames();
                    SetFilePaths();

                    EncryptFile(strFilePath, strEncryptedFilePath, strKey);
                    DecryptFile(strEncryptedFilePath, strDecryptedFilePath, strKey);

                    OutputConsole();
                    
                    SetBackVariables();                    
                }                
            }
            catch (Exception)
            {
                Console.WriteLine("error: " + strDir);
                SetBackVariables();
            }
        }

        private static void CheckIfFolderExists()
        {
            if (!Directory.Exists(strEncryptedFolderPath))
            {
                CreateEncryptedFolderIfNotExists();
            }
            if (!Directory.Exists(strDecryptedFolderPath))
            {
                CreateDecryptedFolderIfNotExists();
            }
        }

        private static void CreateEncryptedFolderIfNotExists()
        {
            Directory.CreateDirectory(strEncryptedFolderPath);
        }

        private static void CreateDecryptedFolderIfNotExists()
        {
            Directory.CreateDirectory(strDecryptedFolderPath);

        }

        private static void SetFileNames()
        {
            strFileName = Path.GetFileName(strFilePath);
            strEncryptedFileName += strFileName;
            strDecryptedFileName += strFileName;
        }

        private static void SetFilePaths()
        {
            strEncryptedFilePath = strEncryptedFolderPath + strEncryptedFileName;
            strDecryptedFilePath = strDecryptedFolderPath + strDecryptedFileName;
        }
        
        private static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // failed to encrypt file
            }
        }

        private static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // failed to decrypt file
            }
        }

        private static void OutputConsole()
        {
            Console.WriteLine(strFilePath);
            Console.WriteLine(strFileName);
            Console.WriteLine(strEncryptedFilePath);
            if (!File.Exists(strEncryptedFilePath))
            {
                Console.WriteLine("Encrypted File Creating...");
            }
            else
            {
                Console.WriteLine("Encrypted File Exists!");
            }

            Console.WriteLine(strDecryptedFilePath);
            if (!File.Exists(strDecryptedFilePath))
            {
                Console.WriteLine("Decrypted File Creating...\n\n");
            }
            else
            {
                Console.WriteLine("Decrypted File Exists!\n\n");
            }
        }

        private static void SetBackVariables()
        {
            strEncryptedFilePath = null;
            strEncryptedFileName = "Encrypted_";

            strDecryptedFilePath = null;
            strDecryptedFileName = "Decrypted_";

            strFilePath = null;
            strFileName = null;
        }
    }
}
