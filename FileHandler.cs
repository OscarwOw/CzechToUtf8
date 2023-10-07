using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechToUtf8
{
    public sealed class FileHandler
    {
        private static readonly Lazy<FileHandler> lazyInstance = new Lazy<FileHandler>(() => new FileHandler());
        private FileHandler()
        {
        }
        public static FileHandler Instance
        {
            get { return lazyInstance.Value; }
        }

        public static IEnumerable<string> ReadAllLines(string path, Encoding encoding)
        {
            var lines = new List<string>();
            int lineNumber = 0;
            long processedSize = 0;
            long fileSize = GetFileLenght(path);

            using (var reader = new StreamReader(path, encoding))
            {
                string line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    long lineSize = encoding.GetByteCount(line) + encoding.GetByteCount(Environment.NewLine);
                    processedSize += lineSize;
                    if (lineNumber % 15000 == 0)
                    { 
                        Console.WriteLine($"Reading: Processed {lineNumber} lines");
                    }
                    lines.Add(DiacriticRemover.RemoveDiacritics(line));
                }
            }
            Console.WriteLine($"Reading: Completed, {lineNumber.ToString()} lines writen");
            return lines;
        }

        public static void WriteAllLines(string path, IEnumerable<string> lines, Encoding encoding)
        {
            using (var writer = new StreamWriter(path, false, encoding))
            {
                int lineNumber = 0;
                foreach (var line in lines)
                {
                    lineNumber++;
                    if (lineNumber % 15000 == 0)
                    {
                        Console.WriteLine($"Writing: Processed {lineNumber} lines");
                    }
                    writer.WriteLine(line);
                }
            }
            Console.WriteLine("Writing: Completed");
        }

        private static long GetFileLenght(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }


    }
}
