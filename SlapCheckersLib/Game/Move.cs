namespace SlapCheckersLib.Game
{
    public class Move
    {
        public int UserId { get; set; }

        public string FromPosition { get; set; }

        public string ToPosition { get; set; }

        public Move(string moveString)
        {
            string[] movePositions = moveString.Split('-');
            FromPosition = movePositions[0];
            ToPosition = movePositions[1];
        }

        public Move(int userId, string fromPosition, string toPosition)
        {
            UserId = userId;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }   
    }
}
