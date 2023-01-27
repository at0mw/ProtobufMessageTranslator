using System;

namespace ProtobufMessageTranslator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("---// Starting Translator");
            Console.WriteLine(AsciiSig.AsciiSignature);
            Console.ReadKey();
            ConsoleKey response;
            do
            {
                
                Console.Write("---// Would you like to Decode or Encode a Protobuf Message? [e/d]");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.E && response != ConsoleKey.D);

            switch (response)
            {
                case ConsoleKey.E:
                    EncodeProtobuf _encoder = new EncodeProtobuf();
                    _encoder.SelectMessageType();
                    break;
                case ConsoleKey.D:
                    DecodeProtobuf _decoder = new DecodeProtobuf();
                    
                    break;
            }
        }
    }
}