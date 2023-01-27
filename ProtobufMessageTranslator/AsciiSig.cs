using System;
using System.IO;

namespace ProtobufMessageTranslator
{
    public static class AsciiSig
    {
        public static string AsciiSignature { get; }
        static AsciiSig()
        {
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\AsciiSignature.txt");
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\AsciiSignature.txt"));
            //Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
            //Console.WriteLine(path);
            AsciiSignature = File.ReadAllText(path);
        }
    }
}