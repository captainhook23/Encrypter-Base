using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DisableToOpenEncryptedData
{
    internal class Program
    {

        private static string strRoot = "C:\\Dokumente";      

        static void Main(string[] args)
        {

            CheckIfFileOpen();
            Console.ReadLine();
        }

        public static bool TraverseFileSystem(String strDir)
        {
            // 1. Für alle Dateien im aktuellen Verzeichnis            
            try
            {
                using (Stream stream = new FileStream(
                    strDir, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }


        // 2. Für alle Unterverzeichnisse im aktuellen Verzeichnis
        //foreach (String strSubDir in Directory.GetDirectories(strDir))
        //{
        //    // 2a. Statt Console.WriteLine hier die gewünschte Aktion
        //    Console.WriteLine(strSubDir);

        //    // 2b. Rekursiver Abstieg
        //    TraverseFileSystem(strSubDir);

        private static void CheckIfFileOpen()
        {
            while (true)
            {
                foreach (String stringFilePath in Directory.GetFiles(strRoot))
                {
                    if (TraverseFileSystem(stringFilePath) == true)
                    {
                        Console.WriteLine(stringFilePath + " Is Open");
                        Thread.Sleep(1000);
                    }
                }
            }
        }


        private static void DenyOpening()
        {

        }

        private static void WarningMessage()
        {
            //MessageBox.Show("!Do NoT Try To Open any EncryPTed FilES!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
