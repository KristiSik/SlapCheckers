namespace SlapCheckersLib
{
    public enum PacketType
    {
        CreateRoom,
        GetRooms,
        Move
    }
    
    public class Packet
    {
        public PacketType Type { get; set; }

        public byte[] Content { get; set; }


        public object Payload { get; set; }
    }
}