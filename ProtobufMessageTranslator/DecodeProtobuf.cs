using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;
using ProtobufMessageTranslator.Protobuf_Contracts;

namespace ProtobufMessageTranslator
{
    public class DecodeProtobuf
    {
        private static string _decodedString;
        private MessageType _messageType;
        private byte[] _coreMessage;
        public DecodeProtobuf()
        {
            Console.WriteLine("---// Enter Protobuf Message one byte at a time or in a , delimited string:");
            
            int byteNum;
            byte[] codedMessage;
            ConsoleKey response;
            do
            {
                
                Console.Write("---// Would you like to enter the Byte[] as Delimited string or Individual bytes? [D/I]");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.I && response != ConsoleKey.D);
            codedMessage = response == ConsoleKey.I ? DecodeSingleInput() : DecodeDelimited();
            Console.WriteLine("---// EndedInput");

            
            DeserialiseMessageWrapper(codedMessage);

            Console.WriteLine($"Final Decoded Message is: {_decodedString}");
        }

        private byte[] DecodeDelimited()
        {
            string response = Console.ReadLine();
            Regex.Replace(response, @"\s+", "");
            Console.WriteLine("Input = " + response);
            return response.Split(',').Select(x => Convert.ToByte(x)).ToArray();
        }

        private byte[] DecodeSingleInput()
        {
            
            var inputByteList = new List<byte>();
            string response;
            do
            {
                Console.WriteLine("---// Enter Byte: ");
                response = Console.ReadLine();
                if (response != null && int.TryParse(response, out _) && response.Length < 5)
                    inputByteList.Add(Convert.ToByte(response));
            } while (!string.IsNullOrEmpty(response) && !response.Contains(","));
            
            return inputByteList.ToArray();
        }

        private static void DeserialiseMessageWrapper(byte[] messageBytes_)
        {
            MessageWrapper decodedWrapperMessage;
            using (var memoryStream = new MemoryStream(messageBytes_))
            {
                decodedWrapperMessage = Serializer.Deserialize<MessageWrapper>(memoryStream);
            }
            Console.WriteLine($"Decoded Message Wrapper: Inner Message Type = {decodedWrapperMessage.MessageId}");
            switch (decodedWrapperMessage.MessageId)
            {
                case MessageType.AnalogMessage:
                {
                    var message = new AnalogMessage();
                    _decodedString = message.DecodeMessage(decodedWrapperMessage.Message);
                    break; 
                }
                case MessageType.DigitalMessage:
                {
                    var message = new DigitalMessage();
                    _decodedString = message.DecodeMessage(decodedWrapperMessage.Message);
                    break;
                }
                case MessageType.StringMessage:
                {
                    var message = new StringMessage();
                    _decodedString = message.DecodeMessage(decodedWrapperMessage.Message);
                    break;
                }
                default:
                    Console.WriteLine("Not a valid byte Array!");
                    break;
            }
        }

    }
}