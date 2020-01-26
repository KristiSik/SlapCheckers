using System;

namespace SlapCheckersLib.Requests
{
    public abstract class Request
    {
        protected const int RequestTypeBytesLength = 1;

        protected const int PayloadLengthIdentifierLength = 2;

        public abstract RequestType Type { get; }

        public object Payload { get; set; }

        public abstract byte[] PackRequest();

        protected byte[] PackIntoFrame(byte[] payload)
        {
            // +-----------------------------+
            // | 1 byte | 2-3 byte | 4+ byte |
            // +-----------------------------+
            // |  Type  |  Payload | Payload |
            // |        |  Lenght  |         |
            // +-----------------------------+

            byte[] typeBytes = new byte[RequestTypeBytesLength];
            Array.Copy(BitConverter.GetBytes((byte) Type), typeBytes, RequestTypeBytesLength);

            byte[] payloadLengthBytes = new byte[PayloadLengthIdentifierLength];
            Array.Copy(BitConverter.GetBytes(payload.Length), payloadLengthBytes, PayloadLengthIdentifierLength);

            byte[] result = new byte[typeBytes.Length + payloadLengthBytes.Length + payload.Length];

            Array.Copy(typeBytes, result, typeBytes.Length);
            Array.Copy(payloadLengthBytes, 0, result, typeBytes.Length, payloadLengthBytes.Length);
            Array.Copy(payload, 0, result, typeBytes.Length + payloadLengthBytes.Length, payload.Length);

            return result;
        }

        public static Request Unpack(byte[] data)
        {
            var requestType = (RequestType) data[0];

            byte[] contentLengthBytes = new byte[PayloadLengthIdentifierLength];
            Array.Copy(data, RequestTypeBytesLength, contentLengthBytes, 0, PayloadLengthIdentifierLength);
            int contentLength = BitConverter.ToInt16(contentLengthBytes, 0);
            
            byte[] payload = new byte[contentLength];
            Array.Copy(data, RequestTypeBytesLength + PayloadLengthIdentifierLength, payload, 0, contentLength);

            switch(requestType)
            {
                case RequestType.Move:
                    return MoveRequest.UnpackRequest(payload);
                default:
                    throw new Exception("Request type is not supported.");
            }
        }
    }
}