using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace ConsoleApplication1
{
    class Program
    {
        private static int MB = 1048576;

        static void Main(string[] args)
        {
            var tempFolder = @"C:\temp\zipTemp";
            var zipFileFolder = @"C:\temp\zip";

            var random = new Random(23);
            _BuildFiles(tempFolder, random);

            using (var zipFile = new ZipFile())
            {
                foreach (var file in Directory.EnumerateFiles(tempFolder))
                {
                    zipFile.AddFile(file);
                }

                zipFile.Save(Path.Combine(zipFileFolder, "output.zip"));
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static int _FilesInFolder(string folder)
        {
            return Directory.GetFiles(folder).Length;
        }

        private static void _BuildFiles(string tempFolder, Random random)
        {
            if (Directory.Exists(tempFolder))
            {
                _ClearFolder(tempFolder);
            }
            else
            {
                Directory.CreateDirectory(tempFolder);
            }

            for (var i = 0; i < 10000; i++)
            {
                var file = Path.Combine(tempFolder, string.Format("file{0}", i));
                if (random.Next(0, 10) > 8)
                {
                    var contents = new byte[random.Next(MB, 10*MB)];
                    random.NextBytes(contents);
                    File.WriteAllBytes(file, contents);
                }
                else
                {
                    var contents = new byte[random.Next(100, MB/10)];
                    random.NextBytes(contents);
                    File.WriteAllBytes(file, contents);
                }
            }
        }

        private static void _ClearFolder(string folder)
        {
            foreach (var file in Directory.EnumerateFiles(folder))
            {
                File.Delete(file);
            }
        }
    }

    
}
