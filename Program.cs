using CzechToUtf8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string inputFilePath = "CZ.txt";
        string outputFilePath = "output.txt";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding encoding = Encoding.GetEncoding("windows-1250");


        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Input file does not exist: {inputFilePath}");
            return;
        }
        IEnumerable<string> lines = FileHandler.ReadAllLines(inputFilePath, encoding);

        
        //var lines = ReadAllLines(inputFilePath, originalEncoding);
        FileHandler.WriteAllLines(outputFilePath, lines, encoding);

        Console.WriteLine($"Conversion Complete. Converted file is at {outputFilePath}");
    }

    

    
}
