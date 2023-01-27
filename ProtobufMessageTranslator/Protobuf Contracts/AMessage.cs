using ProtobufMessageTranslator.Enums;

namespace ProtobufMessageTranslator.Protobuf_Contracts
{
    public abstract class AMessage
    {
        public abstract MessageType MessageType { get; }
        //public abstract string DecodeMessage(byte[] message_);
    }
}