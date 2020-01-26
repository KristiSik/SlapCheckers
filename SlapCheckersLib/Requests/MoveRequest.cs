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
            byte[] payloadBytes = 

            byte[] baseBytes = base.Pack();
        }
    }
}