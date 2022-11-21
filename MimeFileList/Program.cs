using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Web;

namespace MimeFileList
{
    internal class Program
    {
        static void Main()
        {
            //List<FNameSizeMime> list = _GetRecursFiles(Directory.GetCurrentDirectory());
            List<FNameSizeMime> ls = GetRecursFiles(Directory.GetCurrentDirectory());
            FNameSizeMimeStat.StatisticToConsole(ls);
            Console.ReadKey();
        }
        private static List<FNameSizeMime> GetRecursFiles(string start_path)
        {
            long allSize = 0;
            List<FNameSizeMime> listMime = new List<FNameSizeMime>();
            try
            {
                string[] folders = Directory.GetDirectories(start_path);
                listMime.Add(new FNameSizeMime { Name = start_path });
                foreach (string folder in folders)
                {
                    listMime.AddRange(GetRecursFiles(folder));
                    allSize += listMime.FindLast(n => n.Name == folder).Size;
                }
                string[] files = Directory.GetFiles(start_path);
                foreach (string filename in files)
                {
                    FileInfo file = new FileInfo(filename);
                    long size = file.Length;
                    listMime.Add(new FNameSizeMime { Name = filename, Size = file.Length, MimeMap = MimeMapping.GetMimeMapping(filename) });
                    allSize += file.Length;
                }
                listMime.FindLast(n=> n.Name == start_path).Size = allSize;
                
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return listMime;
        }
    }
}
