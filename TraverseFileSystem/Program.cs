using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraverseFileSystem
{
    internal class Program
    {
        private static string strPathDoc = "C:\\Dokumente";
        private static string strRoot = "C:\\";

        static void Main(string[] args)
        {
            TraverseFileSystem(strPathDoc);
            Console.ReadLine();
        }

        public static void TraverseFileSystem(String strDir)
        {
            try
            {
                // 1. Für alle Dateien im aktuellen Verzeichnis
                foreach (String stringFilePath in Directory.GetFiles(strDir))
                {
                    Console.WriteLine(stringFilePath.GetHashCode());
                }

                //2.Für alle Unterverzeichnisse im aktuellen Verzeichnis
                //foreach (String strSubDir in Directory.GetDirectories(strDir))
                //{
                //    // 2a. Statt Console.WriteLine hier die gewünschte Aktion
                //    Console.WriteLine(strSubDir.GetHashCode());

                //    // 2b. Rekursiver Abstieg
                //    TraverseFileSystem(strSubDir);
                //}
            }
            catch (Exception)
            {
                // 3. Statt Console.WriteLine hier die gewünschte Aktion
                Console.WriteLine("error: " + strDir);
            }
        }
    }
}
