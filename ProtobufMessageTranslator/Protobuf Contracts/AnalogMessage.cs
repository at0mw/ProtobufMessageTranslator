using System.IO;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;
using ProtobufMessageTranslator.Protobuf_Contracts.Interfaces;

namespace ProtobufMessageTranslator.Protobuf_Contracts
{
    [ProtoContract]
    public class AnalogMessage : AMessage, IReturnString
    {
        [ProtoMember(1)]
        public int MessageId { get; set; }
        
        [ProtoMember(2)]
        public int JoinIndex { get; set; }
        
        [ProtoMember(3)]
        public int Value { get; set; }
        
        public override MessageType MessageType => MessageType.AnalogMessage;
        public string DecodeMessage(byte[] message_)
        {
            AnalogMessage decodedAnalogMessage;
            using (var memoryStream = new MemoryStream(message_))
            {
                decodedAnalogMessage = Serializer.Deserialize<AnalogMessage>(memoryStream);
            }

            MessageId = decodedAnalogMessage.MessageId;
            JoinIndex = decodedAnalogMessage.JoinIndex;
            Value = decodedAnalogMessage.Value;
            var decodedMessage = $"Message Type {MessageType} with ID {MessageId}, Join {JoinIndex} and Value {Value}";
            return decodedMessage;
        }
    }
}