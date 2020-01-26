using System;

namespace SlapCheckersLib.Requests
{
    public abstract class Request
    {
        public abstract RequestType Type { get; }

        public object Payload { get; set; }

        public abstract byte[] PackRequest();

        protected byte[] PackIntoFrame(byte[] payload)
        {
            // +-----------------------------+
            // | 1 byte | 2-3 byte | 4+ byte |
            // +-----------------------------+
            // |  Type  |  Зayload | Зayload |
            // |        |  Lenght  |         |
            // +-----------------------------+

            byte[] typeBytes = BitConverter.GetBytes((byte) Type);
            byte[] contentLengthBytes = BitConverter.GetBytes(payload.Length);
            byte[] result = new byte[typeBytes.Length + contentLengthBytes.Length + payload.Length];
            Array.Copy(typeBytes, result, typeBytes.Length);
            Array.Copy(contentLengthBytes, 0, result, typeBytes.Length, contentLengthBytes.Length);
            Array.Copy(payload, 0, result, typeBytes.Length + contentLengthBytes.Length, payload.Length);
            return result;
        }
    }
}