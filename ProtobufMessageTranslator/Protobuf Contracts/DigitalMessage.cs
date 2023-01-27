using System;
using System.IO;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;
using ProtobufMessageTranslator.Protobuf_Contracts.Interfaces;

namespace ProtobufMessageTranslator.Protobuf_Contracts
{
    [ProtoContract]
    public class DigitalMessage : AMessage, IReturnString
    {
        [ProtoMember(1)]
        public int MessageId { get; set; }
        
        [ProtoMember(2)]
        public int JoinIndex { get; set; }
        
        [ProtoMember(3)]
        public bool State { get; set; }
        
        public override MessageType MessageType => MessageType.DigitalMessage;
        public string DecodeMessage(byte[] message_)
        {            
            DigitalMessage decodedDigitalMessage;
            using (var memoryStream = new MemoryStream(message_))
            {
                decodedDigitalMessage = Serializer.Deserialize<DigitalMessage>(memoryStream);
            }

            MessageId = decodedDigitalMessage.MessageId;
            JoinIndex = decodedDigitalMessage.JoinIndex;
            State = decodedDigitalMessage.State;
            //Console.WriteLine("Decoded Digital Message");
            var decodedMessage = $"Message Type {MessageType} with ID {MessageId}, Join {JoinIndex} and State {State}";
            return decodedMessage;
        }
    }
}