using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimeFileList
{
    internal class FNameSizeMime
    {
        private string name;
        private long size;
        private string mimeMap;

        public string Name { get => name; set => name = value; }
        public long Size { get => size; set => size = value; }
        public string MimeMap { get => mimeMap; set => mimeMap = value; }
    }

    internal static class FNameSizeMimeStat
    {
        public static void StatisticToConsole(List<FNameSizeMime> ls)
        {
            var countDictionary = new Dictionary<string, int>();
            var sizeDictionary = new Dictionary<string, long>();
            int countAll = 0;
            foreach (FNameSizeMime fname in ls)
            {
                Console.WriteLine(Path.GetFileName(fname.Name) + "    " + fname.Size + "    " + fname.MimeMap);
            }

            foreach (var e in ls)
            {
                if (e.MimeMap == null)
                    continue;
                if (!countDictionary.ContainsKey(e.MimeMap)) countDictionary[e.MimeMap] = 0;
                countDictionary[e.MimeMap]++;
            }

            foreach (var s in countDictionary.Values)
            {
                countAll += s;
            }

            foreach (var ee in ls)
            {
                if (ee.MimeMap == null)
                    continue;
                if (!sizeDictionary.ContainsKey(ee.MimeMap)) sizeDictionary[ee.MimeMap] = 0;
                sizeDictionary[ee.MimeMap]+=ee.Size;
            }

            foreach (var e in countDictionary)
            {
                Console.WriteLine(e.Key + "\t" + e.Value + "\t" + string.Format("{0:P1}", (double)(e.Value) / (double)(countAll)) 
                    + "\t" + (sizeDictionary[e.Key] / e.Value));
            }
        }


    }

}
