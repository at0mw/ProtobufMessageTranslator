using System;
using System.IO;
using System.Linq;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;
using ProtobufMessageTranslator.Protobuf_Contracts;

namespace ProtobufMessageTranslator
{
    public class EncodeProtobuf
    {
        private MessageType _messageType;
        private AMessage _message;
        public EncodeProtobuf()
        {

        }

        public void SelectMessageType()
        {
            ConsoleKey response;
            do
            {
                
                Console.Write("---// What kind of Message are we encoding? \n Analog, Digital or String? [a/d/s]");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.A && response != ConsoleKey.D && response != ConsoleKey.S);

            switch (response)
            {
                case ConsoleKey.A:
                    _messageType = MessageType.AnalogMessage;
                    InputAnalogMessage();
                    break;
                case ConsoleKey.D:
                    _messageType = MessageType.DigitalMessage;
                    InputDigitalMessage();
                    break;
                case ConsoleKey.S:
                    _messageType = MessageType.StringMessage;
                    InputStringMessage();
                    break;
            }


            byte[] encodedMessage = SerialiseMessage(_message);
            // TODO Encode Message in Wrapper:
            
            var messageWrapper = new MessageWrapper()
            {
                MessageId = _messageType,
                Message = encodedMessage
            };

            byte[] finalMessage = SerialiseMessage(messageWrapper);
            PrintFinalMessage(finalMessage);
        }

        private static void PrintFinalMessage(byte[] finalMessage_)
        {
            Console.WriteLine("Final Encoded Message:");
            var commaSeparated = string.Join(",", finalMessage_.Select(item_ => item_.ToString()));
            Console.WriteLine(commaSeparated);
            Console.WriteLine(Convert.ToBase64String(finalMessage_));
            foreach(var msgByte in finalMessage_)
            {
                Console.Write($"[{msgByte}]");
            }
        }

        private void InputAnalogMessage()
        {
            var messageId = InputMessageId();
            var messageJoin = InputMessageJoin();
            var messageValue = InputAnalogMessageValue();
            
            _message = new AnalogMessage
            {
                MessageId = messageId,
                JoinIndex = messageJoin,
                Value = messageValue
            };
        }
        
        private void InputDigitalMessage()
        {
            var messageId = InputMessageId();
            var messageJoin = InputMessageJoin();
            var messageState = InputDigitalMessageState();
            
            _message = new DigitalMessage
            {
                MessageId = messageId,
                JoinIndex = messageJoin,
                State = messageState
            };
        }
        
        private void InputStringMessage()
        {
            var messageId = InputMessageId();
            var messageJoin = InputMessageJoin();
            var messageValue = InputStringMessageValue();
            
            _message = new StringMessage
            {
                MessageId = messageId,
                JoinIndex = messageJoin,
                Value = messageValue
            };
        }

        private static int InputMessageId()
        {
            string input;
            int messageId;
            do
            {
                Console.Write("Input, as an integer, the Message Id: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out messageId));

            return messageId;
        }
        
        private static int InputMessageJoin()
        {
            int messageJoin;
            string input;
            do
            {
                Console.Write("Input, as an integer, the Message Join Index: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out messageJoin));

            return messageJoin;
        }

        private static int InputAnalogMessageValue()
        {
            int messageValue;
            string input;
            do
            {
                Console.Write("Input, as an integer, the Analog Message Value: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out messageValue));

            return messageValue;
        }
        
        private static bool InputDigitalMessageState()
        {
            bool messageState;
            ConsoleKey response;
            do
            {
                Console.Write("Is the digital state true or false? [t/f] ");
                response = Console.ReadKey(false).Key;
            } while (response != ConsoleKey.T && response != ConsoleKey.F);

            return response == ConsoleKey.T;
        }
        
        private static string InputStringMessageValue()
        {
            Console.Write("Input the String Message value: ");
            var messageValue = Console.ReadLine();
            return messageValue;
        }
        
        private static byte[] SerialiseMessage(AMessage message_)
        {
            byte[] byteArray;
            using (var memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, message_);
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }
    }
}