using System.Text;
using Serilog;
using SlapCheckersLib.Game;

namespace SlapCheckersLib.Requests
{
    public class MoveRequest : Request
    {
        public override RequestType Type => RequestType.Move;
        
        public MoveRequest(Move move)
        {
            Payload = move;
        }

        public override byte[] PackRequest()
        {
            var move = (Move) Payload;
            byte[] payloadBytes = Encoding.UTF8.GetBytes($"{move.FromPosition}-{move.ToPosition}");
            return PackIntoFrame(payloadBytes);
        }

        public static MoveRequest UnpackRequest(byte[] payloadBytes)
        {
            string payloadString = Encoding.UTF8.GetString(payloadBytes);
            Log.Debug($"Parsed payload: {payloadString}");
            return new MoveRequest(new Move(payloadString));
        }
    }
}