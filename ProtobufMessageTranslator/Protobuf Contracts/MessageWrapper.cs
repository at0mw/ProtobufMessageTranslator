using System.Collections.Generic;
using ProtoBuf;
using ProtobufMessageTranslator.Enums;
using ProtobufMessageTranslator.Protobuf_Contracts.Interfaces;

namespace ProtobufMessageTranslator.Protobuf_Contracts
{
    [ProtoContract]
    public class MessageWrapper : AMessage, IReturnDictionary
    {
        [ProtoMember(1)] 
        public MessageType MessageId;
        [ProtoMember(2)] 
        public byte[] Message;


        public override MessageType MessageType => MessageType.MessageWrapper;
        public Dictionary<MessageType, byte[]> DecodeMessage(byte[] message_)
        {
            throw new System.NotImplementedException();
        }
    }
}