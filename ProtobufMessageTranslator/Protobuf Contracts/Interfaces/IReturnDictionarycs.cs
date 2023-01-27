using System.Collections.Generic;
using ProtobufMessageTranslator.Enums;

namespace ProtobufMessageTranslator.Protobuf_Contracts.Interfaces
{
    public interface IReturnDictionary
    {
        Dictionary<MessageType, byte[]> DecodeMessage(byte[] message_);
    }
}