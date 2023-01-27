using System.IO;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;

namespace ProtobufMessageTranslator.Protobuf_Contracts
{
    [ProtoContract]
    public class StringMessage : AMessage
    {
        [ProtoMember(1)]
        public int MessageId { get; set; }
        
        [ProtoMember(2)]
        public int JoinIndex { get; set; }
        
        [ProtoMember(3)]
        public string Value { get; set; }
        
        public override MessageType MessageType => MessageType.StringMessage;
        public string DecodeMessage(byte[] message_)
        {
            StringMessage decodedStringMessage;
            using (var memoryStream = new MemoryStream(message_))
            {
                decodedStringMessage = Serializer.Deserialize<StringMessage>(memoryStream);
            }

            MessageId = decodedStringMessage.MessageId;
            JoinIndex = decodedStringMessage.JoinIndex;
            Value = decodedStringMessage.Value;
            var decodedMessage = $"Message Type {MessageType} with ID {MessageId}, Join {JoinIndex} and Value {Value}";
            return decodedMessage;
        }
    }
}