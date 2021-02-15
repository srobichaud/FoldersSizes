using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Scripts
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"";
            string pathWrite = @"";

            List<DirectoryValue> values = new List<DirectoryValue>();
            string[] dirs = Directory.GetDirectories(path);
            foreach(string dir in dirs){
                values.Add(new DirectoryValue{
                    Directory = dir,
                    Size = (double)(GetDirectorySize(dir) / 1000000000d)
                });
            }
            string contentFile = "";
            values = values.OrderByDescending(m=>m.Size).ToList();
            foreach(var value in values){
                contentFile += value.Directory + " (" + value.Size.ToString("F") + " gb)" + Environment.NewLine;
            }
            
            File.WriteAllText(Path.Combine(pathWrite, "log.txt"), contentFile);
        }

        static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }
    }
}
